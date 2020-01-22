using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Main Data")]
    [SerializeField] private GameObject MainProjectile;
    [SerializeField] private Transform MainFirePoint;
    [SerializeField] private int MainProjectileDamage;
    [SerializeField] private float MainCD;
    [SerializeField] private float MainProjectileForce;
    [SerializeField] private SoundEffect MainSoundEffect;
    private bool MainOnCD = false;

    [Header("Secondary Data")]
    [SerializeField] private GameObject SecondaryProjectile;
    [SerializeField] private Transform SecondaryFirePoint;
    [SerializeField] private int SecondaryProjectileDamage;
    [SerializeField] private float SecondaryCD;
    [SerializeField] private float SecondaryProjectileForce;
    [SerializeField] private SoundEffect SecondarySoundEffect;
    private bool SecondaryOnCD = false;

    private void Start()
    {
        this.transform.LookAt(Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 100f)));
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Mouse0) && !MainOnCD)
        {
            FireMainProjectile();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && !SecondaryOnCD)
        {
            FireSecondaryProjectile();
        }
    }

    private void FireMainProjectile()
    {
        GameObject Projectile = Instantiate(MainProjectile, MainFirePoint.position, MainFirePoint.rotation, null);
        Projectile.GetComponent<Rigidbody>().AddForce(Projectile.transform.forward * MainProjectileForce);
        Projectile.GetComponent<SimpleBullet>().Damage = MainProjectileDamage;
        AudioManager.Instance.PlayClipOnce(MainSoundEffect, this.gameObject);

        MainOnCD = true;
        Invoke("ResetMainCD", MainCD);
    }

    private void FireSecondaryProjectile()
    {
        GameObject Projectile = Instantiate(SecondaryProjectile, SecondaryFirePoint.position, SecondaryFirePoint.rotation, null);
        Projectile.GetComponent<Rigidbody>().AddForce(Projectile.transform.forward * SecondaryProjectileForce);

        AudioManager.Instance.PlayClipOnce(SecondarySoundEffect, this.gameObject);

        SecondaryOnCD = true;
        Invoke("ResetSecondaryCD", SecondaryCD);
    }

    void ResetMainCD()
    {
        MainOnCD = false;
    }

    void ResetSecondaryCD()
    {
        SecondaryOnCD = false;
    }
}
