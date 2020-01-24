using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IKillable
{
    [SerializeField] GameObject WeaponRoot;
    [SerializeField] Weapon Weapon;

    int currentHealth = 0;
    PlayerData playerData;

    public int CurrentHealth { get => currentHealth; set => currentHealth = value; }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.Player = this.gameObject;
        playerData = GameManager.Instance.GetPlayerData();
        currentHealth = playerData.MaxHealth;

        if (playerData.EquippedWeapon != null)
        {
            EquipWeapon(playerData.EquippedWeapon);
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Weapon.FireMainProjectile();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Weapon.FireSecondaryProjectile();
        }
    }

    // Aseta uusi Ase Data Weapon scriptiin (ja enabloi WeaponRoot GameObjekti
    public void EquipWeapon(WeaponData data)
    {
        playerData.EquippedWeapon = data;
        WeaponRoot.SetActive(true);
        Weapon.ChangeWeaponData(data);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<IInteractable>() != null)
        {
            other.gameObject.GetComponent<IInteractable>().OnEnterInteract();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<IInteractable>() != null)
        {
            other.gameObject.GetComponent<IInteractable>().OnStayInteract();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<IInteractable>() != null)
        {
            other.gameObject.GetComponent<IInteractable>().OnExitInteract();
        }
    }

    public void OnDamageTaken(int dmgValue)
    {
        CurrentHealth -= dmgValue;

        if (CurrentHealth <= 0)
        {
            OnDeath();
        }
    }

    public void OnDeath()
    {
        throw new System.NotImplementedException();
    }
}
