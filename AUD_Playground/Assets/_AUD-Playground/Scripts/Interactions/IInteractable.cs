using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    void OnEnterInteract();
    void OnStayInteract();
    void OnExitInteract();
}
