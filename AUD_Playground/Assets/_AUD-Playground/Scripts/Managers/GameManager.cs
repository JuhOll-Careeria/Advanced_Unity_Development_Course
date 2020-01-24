using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private PlayerData PlayerData;
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
}
