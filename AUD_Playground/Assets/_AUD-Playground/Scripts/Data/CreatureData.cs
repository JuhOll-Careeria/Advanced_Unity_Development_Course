using UnityEngine;

/// <summary>
/// Base Creature Data that is the parent class to all other creatures
/// </summary>
public class CreatureData : ScriptableObject
{
    [Header("Base Creature Data")]
    public string Name = "Creature Name";
    public int MaxHealth = 50;
    public int Armor = 0;
    public SoundEffect OnHitSound;
    public SoundEffect OnDeathSound;
}
