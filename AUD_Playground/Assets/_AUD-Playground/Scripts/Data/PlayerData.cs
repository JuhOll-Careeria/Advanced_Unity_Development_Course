using UnityEngine;

/// <summary>
/// Base Class for Player Data
/// </summary>
/// 
// CreateAssetMenu allows us to create a new "Enemy Data" asset inside the project window -> Usually we only want one PlayerData to be created at a time
[CreateAssetMenu(fileName = "NewPlayerData", menuName = "AUD/CreatePlayerData", order = 1)]
public class PlayerData : CreatureData
{
    [Header("Player Data")]
    [Range(0, 100)]
    public int MaxStamina = 50;

    // Data we want to transfer over between scenes
    [HideInInspector] public WeaponData EquippedWeapon = null;
    [HideInInspector] public int CurrentHealth = 0;
}
