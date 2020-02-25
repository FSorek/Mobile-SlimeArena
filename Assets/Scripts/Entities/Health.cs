using System;
using UnityEngine;

public class Health
{
    public event Action OnDeath = delegate { };
    public event Action<int> OnTakeDamage = delegate { };

    private readonly int maxHealth;
    private int currentHealth;
    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;
    public bool IsDead => currentHealth <= 0;

    public Health(int maxHealth)
    {
        this.maxHealth = maxHealth;
    }

    public void Reset()
    {
        currentHealth = maxHealth;
    }

    public void Restore(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        OnTakeDamage(damage);
        if (IsDead)
        {
            OnDeath();
        }
    }
}