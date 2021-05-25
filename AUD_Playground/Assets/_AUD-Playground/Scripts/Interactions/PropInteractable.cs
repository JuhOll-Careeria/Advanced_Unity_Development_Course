using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that handles the OnEnter and OnExit interaction of props
/// Example => Add a "Button" that opens a door, player needs to carry a prop that has the 
/// weight equal to "MinRequiredMass". If it is equal or greater, then "Trigger" the button
/// </summary>
public class PropInteractable : BaseInteractable
{
    // minimum required mass to "interact" with this objects
    [SerializeField] float MinRequiredMass = 1f;

    /// <summary>
    /// Compare the mass of the prop placed on an interactable object
    /// </summary>
    /// <param name="ObjMass"></param>
    /// <returns></returns>
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
