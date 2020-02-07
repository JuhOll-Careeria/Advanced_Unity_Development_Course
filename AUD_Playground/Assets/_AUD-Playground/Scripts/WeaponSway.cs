using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    [SerializeField] float Sway = 0.055f;
    [SerializeField] float SwayMax = 0.09f;
    [SerializeField] float SwaySmooth = 3f;

    Vector3 def;
    Vector3 defAth;
    Vector3 Euler;

    // Start is called before the first frame update
    void Start()
    {
        def = transform.localPosition;
        Euler = transform.localEulerAngles;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float factorX = Input.GetAxis("Mouse X") * Sway;
        float factorY = Input.GetAxis("Mouse Y") * Sway;

        if (factorX > SwayMax) { factorX = SwayMax; }
        if (factorX < -SwayMax) { factorX = -SwayMax; }
        if (factorY > SwayMax) { factorX = SwayMax; }
        if (factorY < -SwayMax) { factorX = -SwayMax; }

        Vector3 final = new Vector3(def.x + factorX, def.y + factorY, def.z);
        transform.localPosition = Vector3.Lerp(transform.localPosition, final, Time.deltaTime * SwaySmooth);
    }
}
