using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private PlayerData PlayerData;

    [Header("Physics")]
    [SerializeField] List<BulletHole> BulletHoles = new List<BulletHole>();

    private GameObject playerReference;

    private void Start()
    {
        PlayerData.EquippedWeapon = null;
    }

    public PlayerData GetPlayerData()
    {
        return PlayerData;
    }

    public GameObject Player
    {
        get
        {
            return playerReference;
        }
        set
        {
            playerReference = value;
            Debug.Log("Player added to GameManager", value);
        }
    }

    public GameObject GetBulletHolePrefab(PhysicMaterial physicMat)
    {
        foreach (BulletHole BH in BulletHoles)
        {
            if (BH.PhysicMat.Equals(physicMat))
            {
                return BH.BulletHolePrefab;
            }
        }

        // If the material is not on the list, return the base bullet hole impact prefab
        return BulletHoles[0].BulletHolePrefab;
    }

    [System.Serializable]
    public class BulletHole
    {
        public PhysicMaterial PhysicMat;
        public GameObject BulletHolePrefab;
    }
}
