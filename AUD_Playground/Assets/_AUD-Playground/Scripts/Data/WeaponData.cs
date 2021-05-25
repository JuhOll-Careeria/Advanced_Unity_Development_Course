using UnityEngine;

/// <summary>
/// Weapon scriptable object class, which allows us to create and manage multiple different weapons
/// </summary>
// CreateAssetMenu allows us to create a new "Weapon Data" asset inside the project window
[CreateAssetMenu(fileName = "NewWeaponData", menuName = "AUD/CreateWeaponData", order = 0)]
public class WeaponData : ScriptableObject
{
    [Header("Prefab")]
    public GameObject _WepPrefab; // Visible prefab of the weapon, that is spawned on the characters "hands"

    // Main fire variables (Left Mouse Button)
    [Header("Main Fire")]
    public int _MainFireDamage; // damage of the weapon
    public float _MainFireCD; // cooldown of the fire. Smaller value => shoots more often
    public SoundEffect _MainFireSE;
    public SoundEffect _MainReloadSE; 
    public int MainMaxAmmo = 30; // max ammo per clip
    public float MainReloadTime = 3f; //reload time
    // If this weapon is a sniper, toggle "SnipeScope" => true
    public bool SniperScope = false;
    public float ScopedZoomFOV = 20f;

    // If the weapon has an alternate fire mode (In the project, rifle has a "grenade launcher")
    // leave "False" if no secondary projectile fire is in use
    [Header("Secondary Fire")]
    public bool _UseSecondaryFire = false;
    public GameObject _SecondaryProjectile;
    public int _SecondaryProjectileDamage;
    public float _SecondaryCD;
    public float _SecondaryProjectileForce;
    public SoundEffect _SecondaryFireSE;
    public SoundEffect _SecondaryReloadSE;
    public int SecondaryMaxAmmo = 2;
    public float SecondaryReloadTime = 3f;
}
