using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;


public class LevelManager : Singleton<LevelManager>
{
    // Tapa 1 toteuttaa Scene Referencen käyttö
    // [SerializeField] private SceneReference Level;
    // Sitten helposti vaihdetaan sceneä jostain metodista "SceneManager.LoadScene(Level);"

    /// <summary>
    /// Toinen tapa toteuttaa SceneReference datan hallinta. Tämä tapa on enemmänkin sellaisille peleille jossa on monia 
    /// Eri pelikenttiä, jossa hyppäät eri levelien välillä jatkuvasti.
    /// Tällä tavalla voidaan lataa leveli simppelisti stringiä käyttäen (Stringin ei siis tarvitse olla saman niminen kuin scene tiedosto)
    /// Esimerkki: LoadLevel("Tutorial Level") => Käy tarkistamassa onko LevelDataa jossa "LevelName = "Tutorial Level"", jos on niin lataa sen scenen,
    /// joka on linkitetty "Scene" variableen.
    /// </summary>
    [System.Serializable]
    public class LevelData
    {
        public string LevelName;        // Voit antaa levelille jonkun oman nimen (Ei tarvitse olla sama kuin scene assetissa)
        public SceneReference Scene;    // Linkitä scene tiedosto tähän inspectorissa kätevästi Drag and Dropilla
    }

    [SerializeField] private LevelData MainMenu;
    // Array List kentistä, joka hyödyntää LevelData luokan variableja. 
    // Voidaan hakea kymmeniä kenttiä näppärästi, esim stringin tai SceneReferencen tiedoilla
    [SerializeField] private LevelData[] Levels;

    [SerializeField] Volume PostProcessVolume;
    [SerializeField] float PortalEffectDuration;
    [SerializeField] float PortalEffectFactor;

    ChromaticAberration chromaticAberration;
    LensDistortion lensDistortion;
    string PortalSceneReference;
    bool UsingPortal;

    void Start()
    {
        PostProcessVolume.sharedProfile.TryGet(out chromaticAberration);
        PostProcessVolume.sharedProfile.TryGet(out lensDistortion);
        chromaticAberration.intensity.value = 0;
        lensDistortion.intensity.value = 0;
        SceneManager.sceneLoaded += OnLevelLoaded;
        LoadMainMenu();
    }

    /// <summary>
    /// Lataa leveli stringillä
    /// </summary>
    /// <param name="name">Nimi on tämä</param>
    public void LoadLevel(string name)
    {
        foreach (LevelData data in Levels)
        {
            if (data.LevelName.Equals(name))
            {
                SceneManager.LoadScene(data.Scene);
                return;
            }
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(MainMenu.Scene);
    }

    public void LoadLevel(SceneReference scene)
    {
        SceneManager.LoadScene(scene);
    }

    void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.path == MainMenu.Scene.ScenePath)
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            UIManager.Instance.ToggleHUD(false);
            UIManager.Instance.ToggleOptionsUI(true);
        }
        else
        {
            UIManager.Instance.ToggleHUD(true);
            UIManager.Instance.ToggleOptionsUI(false);
        }
    }

    /// <summary>
    /// Used for changing scenes via portals
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="UsePortalEffect"></param>
    public void LoadLevel(string scene, bool UsePortalEffect)
    {
        if (UsePortalEffect)
        {
            PortalSceneReference = scene;
            DoPortalEffect();
        }
    }

    private void Update()
    {
        if (UsingPortal)
        {
            lensDistortion.intensity.value = Mathf.Lerp(lensDistortion.intensity.value, -1, PortalEffectDuration * Time.deltaTime);
            chromaticAberration.intensity.value = Mathf.Lerp(chromaticAberration.intensity.value, 1, PortalEffectDuration * Time.deltaTime);
        }
        else
        {
            lensDistortion.intensity.value = Mathf.Lerp(lensDistortion.intensity.value, 0, PortalEffectDuration * Time.deltaTime);
            chromaticAberration.intensity.value = Mathf.Lerp(chromaticAberration.intensity.value, 0, PortalEffectDuration * Time.deltaTime);
        }
    }

    void DoPortalEffect()
    {
        UsingPortal = true;
        GameManager.Instance.TogglePlayerInput(false);
        Invoke("LoadLevel", PortalEffectDuration);
    }

    /// <summary>
    /// after a certain time change the level using the portal scene reference and reset chromatic abberration
    /// </summary>
    void LoadLevel()
    {
        UsingPortal = false;
        GameManager.Instance.TogglePlayerInput(true);
        LoadLevel(PortalSceneReference);
    }
}
