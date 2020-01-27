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

    GameObject CurrentWeaponMesh = null;

    public WeaponData GetWeaponData()
    {
        return WeaponData;
    }

    private void Start()
    {
        this.transform.LookAt(Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 100f)));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (WeaponData != null)
            {
                if (PrimaryCurrentAmmo < WeaponData.MainMaxAmmo || SecondaryCurrentAmmo < WeaponData.SecondaryMaxAmmo)
                {
                    DoReload();
                }
            }
        }
    }

    public void ChangeWeaponData(WeaponData data)
    {
        if (CurrentWeaponMesh != null)
        {
            Destroy(CurrentWeaponMesh);
        }

        WeaponData = data;
        CurrentWeaponMesh = Instantiate(data._WepPrefab, this.transform);

        // TODO: Syntiä
        foreach (Transform trans in GetComponentsInChildren<Transform>())
        {
            if (trans.name.Equals("FirePoint_Main"))
                MainFirePoint = trans;

            if (trans.name.Equals("FirePoint_Secondary") && data._UseSecondaryFire)
                SecondaryFirePoint = trans;

            if (trans.GetComponent<ParticleSystem>())            
                MainFirePointPS = trans.GetComponent<ParticleSystem>();                    
        }

        PrimaryCurrentAmmo = data.MainMaxAmmo;
        SecondaryCurrentAmmo = data.SecondaryMaxAmmo;
        RefreshUI();
    }

    public void FireMainProjectile()
    {
        if (MainOnCD || MainReloading)
            return;


        RegisterRayCast();

        PrimaryCurrentAmmo--;
        RefreshUI();

        AudioManager.Instance.PlayClipOnce(WeaponData._MainFireSE, this.gameObject);
        MainFirePointPS.Play();

        // Do reload instead of setting MainOnCD to true
        if (PrimaryCurrentAmmo <= 0)
        {
            MainReloading = true;
            AudioManager.Instance.PlayClipOnce(WeaponData._MainReloadSE, this.gameObject);
            Invoke("ReloadMain", WeaponData.MainReloadTime);
            return;
        }

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


        GameObject Projectile = Instantiate(WeaponData._SecondaryProjectile, SecondaryFirePoint.position, SecondaryFirePoint.rotation, null);
        Projectile.GetComponent<ISecondaryProjectile>().SetDamage(WeaponData._SecondaryProjectileDamage);
        Projectile.GetComponent<Rigidbody>().AddForce(Projectile.transform.forward * WeaponData._SecondaryProjectileForce);

        SecondaryCurrentAmmo--;

        AudioManager.Instance.PlayClipOnce(WeaponData._SecondaryFireSE, this.gameObject);

        RefreshUI();

        // If we run out of ammo, do reload instead of setting "SecondaryOnCD"
        if (SecondaryCurrentAmmo <= 0)
        {
            SecondaryReloading = true;
            AudioManager.Instance.PlayClipOnce(WeaponData._SecondaryReloadSE, this.gameObject);
            Invoke("ReloadSecondary", WeaponData.SecondaryReloadTime);
            return;
        }

        SecondaryOnCD = true;
        Invoke("ResetSecondaryCD", WeaponData._SecondaryCD);
    }

    void RefreshUI()
    {
        UIManager.Instance.RefreshAmmoUI(WeaponData, PrimaryCurrentAmmo, SecondaryCurrentAmmo);
    }

    public void DoReload()
    {
        MainReloading = true;
        AudioManager.Instance.PlayClipOnce(WeaponData._MainReloadSE, this.gameObject);
        Invoke("ReloadMain", WeaponData.MainReloadTime);

        SecondaryReloading = true;
        AudioManager.Instance.PlayClipOnce(WeaponData._SecondaryReloadSE, this.gameObject);
        Invoke("ReloadSecondary", WeaponData.SecondaryReloadTime);
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
