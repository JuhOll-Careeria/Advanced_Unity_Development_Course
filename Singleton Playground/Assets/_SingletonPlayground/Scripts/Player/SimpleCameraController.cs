using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Simple Player Camera Controller
/// </summary>
public class SimpleCameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    public float pitch = 2f;

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = target.position - offset;
        transform.LookAt(target.position + Vector3.up * pitch);
    }
}
