using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public InteractionEvent onInteraction;

    public static EventManager Instance;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        
        else
            Destroy(gameObject);
    }
}
