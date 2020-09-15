using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropInteractable : BaseInteractable
{
    [SerializeField] float MinRequiredMass = 1f;

    public bool CompareMass(float ObjMass)
    {
        if (ObjMass >= MinRequiredMass)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override void OnEnterInteract()
    {
        base.OnEnterInteract();
    }

    public override void OnExitInteract()
    {
        base.OnExitInteract();
    }

    public override void OnStayInteract() { }
}
