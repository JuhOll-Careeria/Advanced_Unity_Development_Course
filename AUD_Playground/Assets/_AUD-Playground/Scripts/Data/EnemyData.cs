using UnityEngine;

/// <summary>
/// Base class for Enemy Data
/// </summary>
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

    public bool CalculateCrit()
    {
        float rand = Random.Range(0.00f, 1.00f);

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
