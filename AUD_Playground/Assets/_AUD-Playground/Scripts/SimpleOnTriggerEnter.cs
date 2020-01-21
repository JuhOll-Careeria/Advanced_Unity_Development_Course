using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Just a simple example OnTriggerEnter script
/// </summary>
public class SimpleOnTriggerEnter : MonoBehaviour
{
    [SerializeField] private List<string> TriggerTags = new List<string>();

    public SoundEffect BounceClip;

    private void OnTriggerEnter(Collider other)
    {
        if (TriggerTags.Contains(other.gameObject.tag))
        {
            UIManager.Instance.AddBounce();
            AudioManager.Instance.PlayClipOnce(BounceClip, this.gameObject);
        }
    }
}
