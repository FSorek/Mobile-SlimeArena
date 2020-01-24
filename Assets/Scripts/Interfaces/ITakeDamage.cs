using System;

public interface ITakeDamage
{
    event Action OnDeath;
    void TakeDamage(int damage);
    bool IsDead { get; }
}