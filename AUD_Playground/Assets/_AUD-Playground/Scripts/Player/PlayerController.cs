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
    bool scoping = false;

    int currentHealth = 0;
    PlayerData playerData;
    float BaseFOV;

    PlayerControls Controls;
    bool Firing = false;
    public int CurrentHealth { get => currentHealth; set => currentHealth = value; }

    private void Awake()
    {
        Controls = new PlayerControls();

        Controls.Player.Fire.performed += ctx => Firing = true;
        Controls.Player.Fire.canceled += ctx => Firing = false;
        Controls.Player.CarryObject.performed += ctx => TryPickup();
        Controls.Player.ThrowObject.performed += ctx => ThrowObject();
        Controls.Player.SecondaryFire.performed += ctx => DoSecondaryFire();
    }

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
        BaseFOV = PlayerCam.fieldOfView;
    }

    void Update()
    {
        if (Firing)
            DoMainFire();

    }

    private void FixedUpdate()
    {
        if (carrying)
            CarryObject();

    }

    void DoMainFire()
    {
        if (!Weapon.GetWeaponData())
            return;

        Weapon.FireMainProjectile();
    }

    void DoSecondaryFire()
    {
        if (!Weapon.GetWeaponData())
            return;

        if (Weapon.GetWeaponData().SniperScope)
        {
            UIManager.Instance.ToggleSnipeScope();
        }
        else
        {
            Weapon.FireSecondaryProjectile();
        }
    }

    public void SetScopedFOV(bool t)
    {
        if (t)
        {
            GetComponent<FirstPersonAIO>().mouseSensitivity = 0.2f;
            PlayerCam.fieldOfView = Weapon.GetWeaponData().ScopedZoomFOV;
        }
        else
        {
            GetComponent<FirstPersonAIO>().mouseSensitivity = 1.4f;
            PlayerCam.fieldOfView = BaseFOV;
        }
    }

    void CarryObject()
    {
        PickedObject.GetComponent<Rigidbody>().useGravity = false;
        PickedObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        PickedObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Vector3 pos = PlayerCam.transform.position + PlayerCam.transform.forward * CarryDistance;
        PickedObject.transform.position = Vector3.Lerp(PickedObject.transform.position, pos, Time.deltaTime * CarrySmooth);
    }

    void DropObject()
    {
        if (!carrying)
            return;

        carrying = false;
        PickedObject.GetComponent<Rigidbody>().useGravity = true;
        PickedObject = null;
    }

    void ThrowObject()
    {
        if (!carrying)
            return;

        Rigidbody ObjectRB = PickedObject.GetComponent<Rigidbody>();
        DropObject();
        ObjectRB.AddForce(PlayerCam.transform.forward * ThrowForce);
    }

    void TryPickup()
    {
        if (carrying)
        {
            DropObject();
            return;
        }

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

    }

    private void OnEnable()
    {
        Controls.Enable();
    }

    private void OnDisable()
    {
        Controls.Disable();
    }
}
