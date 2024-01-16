using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    void Start()
    {
        EventManager.Instance.onInteraction.AddListener(OnInteraction);
    }

    private void OnInteraction(GameObject interactingObject)
    {
        switch (interactingObject.tag)
        {
            case "Slot":
                Debug.Log($"Interacted with {interactingObject.name}");
                break;

            default:

                break;
        }

    }
}
