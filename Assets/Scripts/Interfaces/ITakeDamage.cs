using System;
using UnityEngine;

public interface ITakeDamage
{
    event Action<int> OnTakeDamage;
    int CurrentHealth { get; }
    int MaxHealth { get; }
    void TakeDamage(ICanAttack source, int damage);
}