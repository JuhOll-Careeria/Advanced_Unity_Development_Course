using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    void Start()
    {
        LoadLevel("Tutorial");
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

    public void LoadLevel(SceneReference scene)
    {
        SceneManager.LoadScene(scene);
    }
}
