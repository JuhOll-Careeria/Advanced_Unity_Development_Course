using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Just a simple example OnTriggerEnter script
/// </summary>
public class SimpleOnTriggerEnter : MonoBehaviour
{
    [SerializeField] private UnityEvent Events;
    [SerializeField] private List<string> TriggerTags = new List<string>();

    private void OnTriggerEnter(Collider other)
    {
        if (TriggerTags.Contains(other.gameObject.tag))
        {
            Events.Invoke();
        }
    }
}
