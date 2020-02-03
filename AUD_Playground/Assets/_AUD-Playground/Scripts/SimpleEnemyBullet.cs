using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyBullet : MonoBehaviour
{
    public int Damage;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Enemy"))
        {
            if (collision.gameObject.GetComponent<IKillable>() != null)
            {
                collision.gameObject.GetComponent<IKillable>().OnDamageTaken(Damage);
            }

            Destroy(this.gameObject);
        }
    }
}