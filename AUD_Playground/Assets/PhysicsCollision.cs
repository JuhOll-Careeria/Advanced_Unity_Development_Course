using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCollision : MonoBehaviour
{
    [SerializeField] SoundEffect OnCollisionEnterSound;

    AudioSource AS;
    Rigidbody RB;

    bool isDragging = false;

    private void Start()
    {
        RB = GetComponent<Rigidbody>();
        AS = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (RB.velocity.magnitude > 0.5f)
        {
            AudioManager.Instance.PlayClipOnce(OnCollisionEnterSound, this.gameObject);
        }

        Debug.Log(RB.velocity.magnitude);
    }
}
