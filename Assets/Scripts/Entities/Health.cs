using System;
using UnityEngine;

public class Health
{
    public event Action OnDeath = delegate { };
    public event Action<int> OnTakeDamage = delegate { };

    private readonly int maxHealth;
    private readonly float invincibilityDuration;
    private int currentHealth;
    private float lastTimeTookDamage;
    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;
    public bool IsDead => currentHealth <= 0;

    public Health(int maxHealth, float invincibilityDuration)
    {
        this.maxHealth = maxHealth;
        this.invincibilityDuration = invincibilityDuration;
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

    public virtual void TakeDamage(int damage)
    {
        if(Time.time - lastTimeTookDamage < invincibilityDuration)
            return;
        
        currentHealth -= damage;
        OnTakeDamage(damage);
        if (IsDead)
        {
            OnDeath();
        }

        lastTimeTookDamage = Time.time;
    }
}