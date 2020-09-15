using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Prop : MonoBehaviour
{
    [SerializeField] float ObjectMass = 1f;

    Rigidbody RB;

    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody>();
        RB.mass = ObjectMass;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PropInteractable>() != null)
        {
            PropInteractable prop = other.gameObject.GetComponent<PropInteractable>();

            if (prop.CompareMass(ObjectMass))
            {
                prop.OnEnterInteract();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<PropInteractable>() != null)
        {
            PropInteractable prop = other.gameObject.GetComponent<PropInteractable>();

            if (prop.CompareMass(ObjectMass))
            {
                prop.OnExitInteract();
            }
        }
    }
}
