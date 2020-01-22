using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleGrenade : MonoBehaviour
{
    public GameObject ExplosionEffect;
    public float ExplosionRadius = 5f;
    public float ExplosionPower = 10f;
    public int Damage = 0;

    public void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            Detonate();
        }
    }

    public void Detonate()
    {
        Debug.Log("Explosion!");
        Instantiate(ExplosionEffect, this.transform.position, this.transform.rotation, null);

        Collider[] colliders = Physics.OverlapSphere(this.transform.position, ExplosionRadius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(ExplosionPower, this.transform.position, ExplosionRadius, 3.0F);
        }

        Destroy(this.gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, ExplosionRadius);        
    }
}
