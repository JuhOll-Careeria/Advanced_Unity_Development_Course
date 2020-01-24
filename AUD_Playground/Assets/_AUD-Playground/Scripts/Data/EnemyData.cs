using UnityEngine;

/// <summary>
/// Base class for Enemy Data
/// </summary>
[CreateAssetMenu(fileName = "NewEnemyData", menuName = "AUD/CreateEnemyData", order = 1)]
public class EnemyData : CreatureData
{
    [Header("Enemy Data")]
    public GameObject ProjectilePrefab;
    public int Damage;
    [Range(0f, 1f)]
    public float CritChance;
    public float CritDmgMultiplier = 2;

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
