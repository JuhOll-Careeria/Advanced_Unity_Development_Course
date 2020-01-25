using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyController : MonoBehaviour, IKillable
{
    [SerializeField] EnemyData Data;

    Rigidbody RB;
    int currentHealth = 0;

    public int CurrentHealth { get => currentHealth; set => currentHealth = value; }


    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody>();
        currentHealth = Data.MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDamageTaken(int dmgValue)
    {
        CurrentHealth -= dmgValue;

        if (CurrentHealth <= 0)
        {
            OnDeath();
        }
    }

    public void OnDeath()
    {
        RB.useGravity = true;
        Destroy(this.gameObject, 5f);
    }
}
