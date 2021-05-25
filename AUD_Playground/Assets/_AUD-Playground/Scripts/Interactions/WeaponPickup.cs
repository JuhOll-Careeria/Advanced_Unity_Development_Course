using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Scripts that handles the pickup of weapons, a simple "OnEnterInteractable" child class
/// </summary>
public class WeaponPickup : OnEnterInteractable
{
    [SerializeField] WeaponData WepData;

    public override void OnEnterInteract()
    {
        base.OnEnterInteract();

        // TODO: Improve this by not always picking up the weapon. Let the player press a button TO pick up and drop the old one to the ground
        // if player is on top of the weapon pickup object, give the weapon to the player
        GameManager.Instance.Player.GetComponent<PlayerController>().EquipWeapon(WepData);
    }
}
