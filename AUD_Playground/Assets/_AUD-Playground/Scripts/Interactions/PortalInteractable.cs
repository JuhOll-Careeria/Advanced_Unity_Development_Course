using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that is attached to the "Portals" that transfer player between scenes
/// </summary>
public class PortalInteractable : OnEnterInteractable
{
    [Header("Portal Data")]
    [SerializeField] string LevelName; // What scene we want to transfer to?

    public override void OnEnterInteract()
    {
        base.OnEnterInteract();
        LevelManager.Instance.LoadLevel(LevelName, true);
    }
}
