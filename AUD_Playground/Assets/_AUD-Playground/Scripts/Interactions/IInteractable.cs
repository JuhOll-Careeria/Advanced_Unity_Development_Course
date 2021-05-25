using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interaface for the interaction system
/// </summary>
public interface IInteractable
{
    void OnEnterInteract();
    void OnStayInteract();
    void OnExitInteract();
}
