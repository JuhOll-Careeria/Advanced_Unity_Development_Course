using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "AUD/CreateWeaponData", order = 0)]
public class WeaponData : ScriptableObject
{
    [Header("Main Fire")]
    public int _MainFireDamage;
    public float _MainFireCD;
    public SoundEffect _MainSoundEffect;
    public int MainMaxAmmo = 30;
    public float MainReloadTime = 3f;

    [Header("Secondary Fire")]
    public bool _UseSecondaryFire = true;
    public GameObject _SecondaryProjectile;
    public int _SecondaryProjectileDamage;
    public float _SecondaryCD;
    public float _SecondaryProjectileForce;
    public SoundEffect _SecondarySoundEffect;
    public int SecondaryMaxAmmo = 2;
    public float SecondaryReloadTime = 3f;

}
