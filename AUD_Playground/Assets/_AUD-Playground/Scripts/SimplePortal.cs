using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePortal : MonoBehaviour
{
    [SerializeField] private List<string> TriggerTags = new List<string>();

    [SerializeField] private SoundEffect PortalEnterSound;

    // Testausta varten oleva variable
    public bool UseSceneReference = false;

    [SerializeField] private string LevelName;
    [SerializeField] private SceneReference Level;

    public void OnTriggerEnter(Collider other)
    {
        if (TriggerTags.Contains(other.gameObject.tag))
        {
            // Testausta varten tehty tarkistus, kattoo jos UseSceneReference on true tai false.
            if (!UseSceneReference)
            {
                // Lataa uusi leveli stringin perusteella
                LevelManager.Instance.LoadLevel(LevelName);
            } else
            {
                // lataa uusi leveli SceneReference perusteella
                LevelManager.Instance.LoadLevel(Level);
            }

            AudioManager.Instance.PlayClipOnce(PortalEnterSound);
        }
    }
}
