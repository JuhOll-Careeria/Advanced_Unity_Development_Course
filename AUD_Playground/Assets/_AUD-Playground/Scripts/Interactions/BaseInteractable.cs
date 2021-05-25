using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Interaction system base class, that handles the interactions of objects
/// Utilizes IInteractable interface, that requires the metods "OnEnterInteract", "OnStayInteract" and "OnExitInteract" to be added
/// </summary>
public class BaseInteractable : MonoBehaviour, IInteractable
{
    // Unity Events that allows us to easily add function and logic to what happens when interacted
    [SerializeField] UnityEvent OnEnterInteractionEvent; 
    [SerializeField] UnityEvent OnStayInteractionEvent;
    [SerializeField] UnityEvent OnExitInteractionEvent;
    [SerializeField] bool DestroyOnInteract = false;

    public virtual void OnEnterInteract()
    {
        OnEnterInteractionEvent.Invoke();

        if (DestroyOnInteract)
            Destroy(this.gameObject);
    }

    public virtual void OnStayInteract()
    {
        OnStayInteractionEvent.Invoke();
    }

    public virtual void OnExitInteract()
    {
        OnExitInteractionEvent.Invoke();
    }
}
