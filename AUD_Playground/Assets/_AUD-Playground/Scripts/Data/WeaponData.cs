using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "AUD/CreateWeaponData", order = 0)]
public class WeaponData : ScriptableObject
{
    [Header("Prefab")]
    public GameObject _WepPrefab;

    [Header("Main Fire")]
    public int _MainFireDamage;
    public float _MainFireCD;
    public SoundEffect _MainFireSE;
    public SoundEffect _MainReloadSE;
    public int MainMaxAmmo = 30;
    public float MainReloadTime = 3f;
    public bool SniperScope = false;
    public float ScopedZoomFOV = 20f;

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
