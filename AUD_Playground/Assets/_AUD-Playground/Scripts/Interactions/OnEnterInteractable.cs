using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Handles just the "OnEnter" interactable logic, without the others in the interface
/// </summary>
public class OnEnterInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] UnityEvent OnEnterInteractionEvent;
    [SerializeField] SoundEffect SoundEffectOnInteract;
    [SerializeField] bool DestroyOnInteract = false;

    public virtual void OnEnterInteract()
    {
        OnEnterInteractionEvent.Invoke();

        // Jos SpatialBlend value on pienempin kuin 0, niin toista Sound Effekti AudioManagerin AudioSourcelta
        if (SoundEffectOnInteract.spatialBlend <= 0)
        {
            AudioManager.Instance.PlayClipOnce(SoundEffectOnInteract);
        }
        else
        {
            // Jos SpatialBlend value on suurempi kuin nolla, niin toista SoundEffect tämän objektin AudioSourcelta
            AudioManager.Instance.PlayClipOnce(SoundEffectOnInteract, this.gameObject);
        }

        if (DestroyOnInteract)
            Destroy(this.gameObject);
    }

    public void OnExitInteract() { }

    public void OnStayInteract() { }
}
