using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.Processors;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

public class PlayerTouchMovement : MonoBehaviour
{
    [SerializeField] private Vector2 joystickSize = new Vector2(300, 300);
    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] private NavMeshAgent player;

    public float currentSpeed;

    private Finger MovementFinger;
    private Vector2 MovementAmount;


    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        ETouch.Touch.onFingerDown += HandleFingerDown;
        ETouch.Touch.onFingerUp += HandleFingerLose;
        ETouch.Touch.onFingerMove += HandleFingerMove;

    }

    private void OnDisable()
    {
        ETouch.Touch.onFingerDown -= HandleFingerDown;
        ETouch.Touch.onFingerUp -= HandleFingerLose;
        ETouch.Touch.onFingerMove -= HandleFingerMove;
        EnhancedTouchSupport.Disable();

    }

    private void HandleFingerMove(Finger aObj)
    {
        if (aObj == MovementFinger)
        {
            Vector2 knobPosition;
            float maxMovement = joystickSize.x / 2f;
            ETouch.Touch currentTouch = aObj.currentTouch;

            if (Vector2.Distance(currentTouch.screenPosition, joystick.RectTransform.anchoredPosition) > maxMovement)
            {
                knobPosition = (currentTouch.screenPosition -
                               joystick.RectTransform.anchoredPosition).normalized * maxMovement;
            }
            else
            {
                knobPosition = currentTouch.screenPosition - joystick.RectTransform.anchoredPosition;
            }

            joystick.Knob.anchoredPosition = knobPosition;
            MovementAmount = knobPosition / maxMovement;
        }
    }

    private void HandleFingerLose(Finger aObj)
    {   
        if(aObj==MovementFinger)
        {
            MovementFinger = null;
            joystick.Knob.anchoredPosition = Vector2.zero;
            joystick.gameObject.SetActive(false);
            MovementAmount = Vector2.zero;
        }

    }
    private void HandleFingerDown(Finger aObj)
    {
        if (MovementFinger == null)
        {
            MovementFinger = aObj;
            MovementAmount = Vector2.zero;
            joystick.gameObject.SetActive(true);
            joystick.RectTransform.sizeDelta = joystickSize;
            joystick.RectTransform.anchoredPosition = ClampStartPosition(aObj.screenPosition);
        }
    }

    private Vector2 ClampStartPosition(Vector2 aStartPosition)
    {
        if (aStartPosition.x < joystickSize.x / 2)
            aStartPosition.x = joystickSize.x / 2;

        if (aStartPosition.y < joystickSize.y / 2)
            aStartPosition.y = joystickSize.y / 2;

        else if (aStartPosition.y > Screen.height - joystickSize.y / 2)
            aStartPosition.y = Screen.height - joystickSize.y / 2;

        return aStartPosition;
    }

    private void Update()
    {
        Vector3 scaledMovement = player.speed*Time.deltaTime*new Vector3(
            MovementAmount.x,
            0,
            MovementAmount.y);

        player.transform.LookAt(player.transform.position + scaledMovement, Vector3.up);
        player.Move(scaledMovement);

        currentSpeed = scaledMovement.magnitude / Time.deltaTime;

        
    }
}
