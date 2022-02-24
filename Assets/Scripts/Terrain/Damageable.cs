using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Damageable
{
    void Damage(int damageAmount, Vector3 damagePosition);
}