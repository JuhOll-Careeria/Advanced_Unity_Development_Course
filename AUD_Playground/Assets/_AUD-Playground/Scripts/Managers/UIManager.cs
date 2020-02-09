using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Linq;

public class UIManager : Singleton<UIManager>
{
    [Header("Menu")]
    [SerializeField] private GameObject MenuUI;

    [Header("Options")]
    [SerializeField] private OptionsVolumeSlider[] VolumeSliders;
    [SerializeField] private TMP_Dropdown QualityDropDown;
    [SerializeField] private TMP_Dropdown ResolutionDropDown;
    [SerializeField] Toggle BloomToggle;
    [SerializeField] Toggle MotionBlurToggle;
    [SerializeField] Volume PostProcessVolume;

    [Header("HUD")]
    [SerializeField] private GameObject GameHUD;
    [SerializeField] private Image HealthBar;
    [SerializeField] private TextMeshProUGUI HealthBarValue;
    [SerializeField] private Image StaminaBar;
    [SerializeField] private TextMeshProUGUI StaminaBarValue;
    [SerializeField] private TextMeshProUGUI MainAmmo;
    [SerializeField] private TextMeshProUGUI SecondaryAmmo;
    [SerializeField] private GameObject SniperScopeHUD;

    MotionBlur _MotionBlur;
    Bloom _Bloom;

    public void ToggleHUD(bool value)
    {
        GameHUD.SetActive(value);
    }

    public void ToggleOptionsUI(bool value)
    {

        MenuUI.SetActive(value);
    }

    private void Start()
    {
        QualityDropDown.value = 1;
        ChangeQuality();
        PostProcessVolume.sharedProfile.TryGet(out _MotionBlur);
        PostProcessVolume.sharedProfile.TryGet(out _Bloom);
        ToggleBloom();
        ToggleMotionBlur();
    }

    public void ToggleSnipeScope()
    {
        SniperScopeHUD.SetActive(!SniperScopeHUD.activeSelf);

        if (SniperScopeHUD.activeSelf)
        {
            GameManager.Instance.Player.GetComponent<PlayerController>().SetScopedFOV(true);
        }
        else
        {
            GameManager.Instance.Player.GetComponent<PlayerController>().SetScopedFOV(false);
        }
    }

    public void ToggleMotionBlur()
    {
        _MotionBlur.active = MotionBlurToggle.isOn;
    }

    public void ToggleBloom()
    {
        _Bloom.active = BloomToggle.isOn;
    }

    public void ChangeQuality()
    {
        QualitySettings.SetQualityLevel(QualityDropDown.value);
    }

    public void DisableSniperScope()
    {
        SniperScopeHUD.SetActive(false);
    }

    public void RefreshHealth(int currentHealth)
    {
        float HPValue = currentHealth / (float)GameManager.Instance.GetPlayerData().MaxHealth;

        HealthBar.fillAmount = HPValue;
        HealthBarValue.text = (HPValue * 100).ToString() + "%";
    }

    public void RefreshStamina(float currentStamina)
    {
        float StaminaValue = (currentStamina / 10) / (float)GameManager.Instance.GetPlayerData().MaxStamina;
        StaminaBar.fillAmount = StaminaValue;
        StaminaBarValue.text = Mathf.RoundToInt((StaminaValue * 100)).ToString() + "%";
    }

    public void RefreshAmmoUI(WeaponData WepData, int MainCurrentAmmo, int SecondaryCurrentAmmo)
    {
        MainAmmo.text = MainCurrentAmmo.ToString() + " / " + WepData.MainMaxAmmo.ToString();
        SecondaryAmmo.text = SecondaryCurrentAmmo.ToString() + " / " + WepData.SecondaryMaxAmmo.ToString();
    }

    // index viittaa OptionsVolumeSlider muuttujien slidereihin ja mixereihin, 0 => master, 1 => SoundEffect ja 2 = Music
    public void ChangeVolumeSettings(int index)
    {
        AudioManager.Instance.ChangeMixerGroupVolume(VolumeSliders[index]._MixerGroupParameter, VolumeSliders[index]._VolumeSlider.value);
    }
}

[System.Serializable]
public class OptionsVolumeSlider
{
    public Slider _VolumeSlider;
    public string _MixerGroupParameter;
}
