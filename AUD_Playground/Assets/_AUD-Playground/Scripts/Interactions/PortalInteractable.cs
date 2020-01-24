using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalInteractable : OnEnterInteractable
{
    [Header("Portal Data")]
    [SerializeField] string LevelName;

    public override void OnEnterInteract()
    {
        base.OnEnterInteract();
        LevelManager.Instance.LoadLevel(LevelName);
    }
}
