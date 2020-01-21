using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePortal : MonoBehaviour
{
    [SerializeField] private List<string> TriggerTags = new List<string>();
    [SerializeField] private SoundEffect PortalEnterSound;
    [SerializeField] private string LevelName;

    public void OnTriggerEnter(Collider other)
    {
        if (TriggerTags.Contains(other.gameObject.tag))
        {
            // Lataa uusi leveli stringin perusteella
            LevelManager.Instance.LoadLevel(LevelName);

            AudioManager.Instance.PlayClipOnce(PortalEnterSound);
        }
    }
}
