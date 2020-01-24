using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Main Data")]
    [SerializeField] private WeaponData WeaponData;
    [SerializeField] private Transform MainFirePoint;
    [SerializeField] private ParticleSystem MainFirePointPS;
    [SerializeField] private LayerMask LayerMask;
    private bool MainOnCD = false;
    private bool MainReloading = false;
    private int PrimaryCurrentAmmo = 0;

    [Header("Secondary Data")]
    [SerializeField] private Transform SecondaryFirePoint;
    private bool SecondaryOnCD = false;
    private bool SecondaryReloading = false;
    private int SecondaryCurrentAmmo = 0;

    public WeaponData GetWeaponData()
    {
        return WeaponData;
    }

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

        if (Input.GetKeyDown(KeyCode.Mouse1) && !SecondaryOnCD && WeaponData._UseSecondaryFire)
        {
            FireSecondaryProjectile();
        }
    }

    public void ChangeWeaponData(WeaponData data)
    {
        WeaponData = data;
        PrimaryCurrentAmmo = data.MainMaxAmmo;
        SecondaryCurrentAmmo = data.SecondaryMaxAmmo;
        RefreshUI();
    }

    public void FireMainProjectile()
    {
        if (MainOnCD || MainReloading)
            return;

        if (PrimaryCurrentAmmo <= 0)
        {
            MainReloading = true;
            Invoke("ReloadMain", WeaponData.MainReloadTime);
            return;
        }

        RegisterRayCast();

        PrimaryCurrentAmmo--;
        RefreshUI();

        AudioManager.Instance.PlayClipOnce(WeaponData._MainSoundEffect, this.gameObject);
        MainFirePointPS.Play();

        MainOnCD = true;
        Invoke("ResetMainCD", WeaponData._MainFireCD);
    }

    void RegisterRayCast()
    {
        Vector3 rayOrigin = new Vector3(0.5f, 0.5f, 0f); // center of the screen
        float rayLength = 500f;

        Ray ray = Camera.main.ViewportPointToRay(rayOrigin);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayLength, LayerMask))
        {
            if (hit.collider.gameObject.GetComponent<SimpleGrenade>())
            {
                hit.collider.gameObject.GetComponent<SimpleGrenade>().Detonate();
                return;
            }

            if (hit.collider.GetComponent<IKillable>() != null)
            {
                hit.collider.GetComponent<IKillable>().OnDamageTaken(WeaponData._MainFireDamage);
            }
            else if (hit.collider.gameObject.GetComponent<Rigidbody>())
            {
                hit.collider.gameObject.GetComponent<Rigidbody>().AddForce(ray.direction * 100f);
            }

            // Get the Bullet HOle Prefab from the GameManager, which stores all the BulletHole prefabs
            GameObject BulletHole = GameManager.Instance.GetBulletHolePrefab(hit.collider.sharedMaterial);

            GameObject holeGO = Instantiate(BulletHole, hit.point, Quaternion.LookRotation(hit.normal));
            holeGO.transform.SetParent(hit.transform, true);
            Destroy(holeGO, Random.Range(2, 4));
        }
        else
        {
            Debug.Log("Did not Hit");
        }
    }

    public void FireSecondaryProjectile()
    {
        if (SecondaryOnCD || !WeaponData._UseSecondaryFire || SecondaryReloading)
            return;

        if (SecondaryCurrentAmmo <= 0)
        {
            SecondaryReloading = true;
            Invoke("ReloadSecondary", WeaponData.SecondaryReloadTime);
            return;
        }

        GameObject Projectile = Instantiate(WeaponData._SecondaryProjectile, SecondaryFirePoint.position, SecondaryFirePoint.rotation, null);
        Projectile.GetComponent<Rigidbody>().AddForce(Projectile.transform.forward * WeaponData._SecondaryProjectileForce);

        SecondaryCurrentAmmo--;

        AudioManager.Instance.PlayClipOnce(WeaponData._SecondarySoundEffect, this.gameObject);

        RefreshUI();

        SecondaryOnCD = true;
        Invoke("ResetSecondaryCD", WeaponData._SecondaryCD);
    }

    void RefreshUI()
    {
        UIManager.Instance.RefreshAmmoUI(WeaponData, PrimaryCurrentAmmo, SecondaryCurrentAmmo);
    }

    void ReloadMain()
    {
        MainReloading = false;
        PrimaryCurrentAmmo = WeaponData.MainMaxAmmo;
        RefreshUI();
    }

    void ReloadSecondary()
    {
        SecondaryReloading = false;
        SecondaryCurrentAmmo = WeaponData.SecondaryMaxAmmo;
        RefreshUI();
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
