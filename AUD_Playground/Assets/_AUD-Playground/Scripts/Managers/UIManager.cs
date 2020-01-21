using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject BaseHUD;

    [SerializeField] private TextMeshProUGUI BounceAmountText; 
    [SerializeField] private int BounceAmount = 0;

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
}
