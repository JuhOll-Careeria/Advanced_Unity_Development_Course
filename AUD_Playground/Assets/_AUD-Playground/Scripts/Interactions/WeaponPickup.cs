using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : OnEnterInteractable
{
    [SerializeField] WeaponData WepData;

    public override void OnEnterInteract()
    {
        base.OnEnterInteract();

        GameManager.Instance.Player.GetComponent<PlayerController>().EquipWeapon(WepData);
    }
}
