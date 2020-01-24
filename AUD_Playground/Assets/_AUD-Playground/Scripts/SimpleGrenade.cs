using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleGrenade : MonoBehaviour
{
    public GameObject ExplosionEffect;
    public float ExplosionRadius = 5f;
    public float ExplosionPower = 10f;
    public int Damage = 10;

    Rigidbody RB;

    private void Start()
    {
        RB = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        SpinObjectInAir();
    }

    /// <summary>
    /// Spin the object realistically, so it will fall to the ground using the current velocity of the object
    /// </summary>
    void SpinObjectInAir()
    {
        float yVel = RB.velocity.y;
        float zVel = RB.velocity.z;
        float xVel = RB.velocity.x;
        float combinedVel = Mathf.Sqrt(xVel * xVel + zVel * zVel);
        float fallAngle = Mathf.Atan2(yVel, combinedVel) * 180f / Mathf.PI;

        transform.eulerAngles = new Vector3(fallAngle, transform.eulerAngles.y, transform.eulerAngles.x);
    }

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

            if (hit.gameObject.GetComponent<IKillable>() != null)
            {
                hit.gameObject.GetComponent<IKillable>().OnDamageTaken(Damage);
            }

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
