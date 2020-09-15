using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour, IKillable
{
    [SerializeField] int _Health = 10;
    [SerializeField] GameObject _ShatteredPrefab;

    public void OnDamageTaken(int dmgValue)
    {
        _Health -= dmgValue;

        if (_Health <= 0)
        {
            OnDeath();
        }
    }

    public void OnDeath()
    {
        GetComponent<Collider>().enabled = false;
        Instantiate(_ShatteredPrefab, this.transform.position, this.transform.rotation, null);
        Destroy(this.gameObject);
    }
}
