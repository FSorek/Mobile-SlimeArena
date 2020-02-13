using System;

public interface ITakeDamage
{
    event Action OnDeath;
    event Action OnTakeDamage;
    void TakeDamage(int damage);
    bool IsDead { get; }
}