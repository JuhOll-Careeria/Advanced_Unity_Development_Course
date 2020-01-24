﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject BaseHUD;

    [SerializeField] private TextMeshProUGUI BounceAmountText; 
    [SerializeField] private int BounceAmount = 0;

    [SerializeField] private TextMeshProUGUI MainAmmo;
    [SerializeField] private TextMeshProUGUI SecondaryAmmo;

    void Start()
    {
        ToggleHUD(true);
    }

    public void ToggleHUD(bool value)
    {
        BaseHUD.SetActive(value);
    }

    public void AddBounce()
    {
        BounceAmount++;
        RefreshUI();
    }

    public void RefreshUI()
    {
        BounceAmountText.text = BounceAmount.ToString();
        // Metodi joka hallinoi UI refreshausta
    }

    public void RefreshAmmoUI(WeaponData WepData, int MainCurrentAmmo, int SecondaryCurrentAmmo)
    {
        MainAmmo.text = MainCurrentAmmo.ToString() + " / " + WepData.MainMaxAmmo.ToString();
        SecondaryAmmo.text = SecondaryCurrentAmmo.ToString() + " / " + WepData.SecondaryMaxAmmo.ToString();
    }
}
