using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IKillable
{
    [SerializeField] GameObject WeaponRoot;
    [SerializeField] Weapon Weapon;
    [SerializeField] Camera PlayerCam;

    [Header("Carrying")]
    [SerializeField] float PickUpDist = 5f;
    [SerializeField] float CarryDistance = 3f;
    [SerializeField] float CarrySmooth = 3f;
    [SerializeField] float ThrowForce = 20f;

    GameObject PickedObject;
    bool carrying = false;

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

        PlayerCam = GetComponentInChildren<Camera>();
    }

    void Update()
    {
        if (carrying)
        {
            CarryObject();

            if (Input.GetKeyDown(KeyCode.E))
            {
                DropObject();
            }
            else if (Input.GetKeyDown(KeyCode.F))
            {
                ThrowObject();
            }
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            TryPickup();
        }
    }

    private void FixedUpdate()
    {
        if (Weapon.GetWeaponData())
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
    }

    void CarryObject()
    {
        PickedObject.GetComponent<Rigidbody>().useGravity = false;
        Vector3 pos = PlayerCam.transform.position + PlayerCam.transform.forward * CarryDistance;
        PickedObject.transform.position = Vector3.Lerp(PickedObject.transform.position, pos, Time.deltaTime * CarrySmooth);
    }
    
    void DropObject()
    {
        carrying = false;
        PickedObject.GetComponent<Rigidbody>().useGravity = true;
        PickedObject = null;
    }

    void ThrowObject()
    {
        Rigidbody ObjectRB = PickedObject.GetComponent<Rigidbody>();
        DropObject();
        ObjectRB.AddForce(PlayerCam.transform.forward * ThrowForce);
    }

    void TryPickup()
    {
        int x = Screen.width / 2;
        int y = Screen.height / 2;

        Ray ray = PlayerCam.ScreenPointToRay(new Vector3(x, y));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, PickUpDist))
        {
            Prop p = hit.collider.GetComponent<Prop>();

            if (p != null)
            {
                carrying = true;
                PickedObject = p.gameObject;
            }
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
