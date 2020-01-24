using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseInteractable : MonoBehaviour, IInteractable
{
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
