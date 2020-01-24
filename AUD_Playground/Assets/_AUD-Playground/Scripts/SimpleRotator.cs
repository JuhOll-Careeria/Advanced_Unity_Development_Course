using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotator : MonoBehaviour
{
    [SerializeField] Vector3 RotateAxis;

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(RotateAxis * Time.deltaTime);
    }
}
