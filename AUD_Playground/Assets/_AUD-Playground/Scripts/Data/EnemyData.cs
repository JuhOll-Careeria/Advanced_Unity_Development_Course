using UnityEngine;

/// <summary>
/// Base class for Enemy Data, child class of CreatureData
/// </summary>
/// 
// CreateAssetMenu allows us to create a new "Enemy Data" asset inside the project window
[CreateAssetMenu(fileName = "NewEnemyData", menuName = "AUD/CreateEnemyData", order = 1)]
public class EnemyData : CreatureData
{
    [Header("Enemy Data")]
    [Range(0f, 1f)]
    public float CritChance;
    public float CritDmgMultiplier = 2;
    public float ChaseTime = 10f;
    public SoundEffect AttackSE;

    [Header("Attack Data")]
    public GameObject ProjectilePrefab;
    public float ProjectileForce = 500f;
    public float AttackCD = 1f;

    // Calculate the critical hit. 
    public bool CalculateCrit()
    {
        float rand = Random.Range(0.00f, 1.00f); // get random number from 0.00 - 1.00

        // if random number is smaller or equal to the enemies crit chance (variable), then apply critical damage, else return false
        if (rand <= CritChance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
