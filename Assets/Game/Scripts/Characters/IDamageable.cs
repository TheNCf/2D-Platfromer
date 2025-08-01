using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public int TakeDamage(int damage);

    public void Heal(int amount);
}
