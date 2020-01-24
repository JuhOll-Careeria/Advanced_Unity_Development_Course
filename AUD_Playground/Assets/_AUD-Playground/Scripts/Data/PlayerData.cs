using UnityEngine;

/// <summary>
/// Base Class for Player Data
/// </summary>
[CreateAssetMenu(fileName = "NewPlayerData", menuName = "AUD/CreatePlayerData", order = 1)]
public class PlayerData : CreatureData
{
    [Header("Player Data")]
    public WeaponData EquippedWeapon = null;

}
