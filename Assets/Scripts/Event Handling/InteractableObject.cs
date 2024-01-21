using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    private SlotBehaviour slotMachine;

    private void Awake()
    {
        slotMachine = FindObjectOfType<SlotBehaviour>();
    }

    void Start()
    {
        EventManager.Instance.onInteraction.AddListener(OnInteraction);
    }

    private void OnInteraction(GameObject interactingObject)
    {
        switch (interactingObject.tag)
        {
            case "Slot":
                if(slotMachine.isAvailable)
                {
                    StartCoroutine(slotMachine.PullTheLever());
                    StartCoroutine(slotMachine.ColorTheScreen());
                }
                break;

            default:

                break;
        }

    }
}
