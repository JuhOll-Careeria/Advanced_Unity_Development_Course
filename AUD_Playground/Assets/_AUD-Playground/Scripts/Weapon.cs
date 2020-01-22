using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Main Data")]
    [SerializeField] private Transform MainFirePoint;
    [SerializeField] private ParticleSystem MainFirePointPS;
    [SerializeField] private int MainFireDamage;
    [SerializeField] private LayerMask LayerMask;
    [SerializeField] private float MainFireCD;
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
        RegisterRayCast();

        AudioManager.Instance.PlayClipOnce(MainSoundEffect, this.gameObject);
        MainFirePointPS.Play();

        MainOnCD = true;
        Invoke("ResetMainCD", MainFireCD);
    }

    void RegisterRayCast()
    {
        Vector3 rayOrigin = new Vector3(0.5f, 0.5f, 0f); // center of the screen
        float rayLength = 500f;

        Ray ray = Camera.main.ViewportPointToRay(rayOrigin);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayLength, LayerMask))
        {
            Debug.Log("Did Hit on " + hit.collider.gameObject.name, hit.collider.gameObject);
        }
        else
        {
            Debug.Log("Did not Hit");
        }
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
