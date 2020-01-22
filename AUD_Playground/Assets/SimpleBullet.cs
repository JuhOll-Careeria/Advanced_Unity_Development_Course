using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleBullet : MonoBehaviour
{
    public int Damage;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit Object and dealt " + Damage + " damage!", collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
