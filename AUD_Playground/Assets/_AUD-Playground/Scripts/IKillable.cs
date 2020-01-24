using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKillable
{
    void OnDamageTaken(int dmgValue);
    void OnDeath();
}
