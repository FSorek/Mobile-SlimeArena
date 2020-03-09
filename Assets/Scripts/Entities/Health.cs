using System;
using UnityEngine;

public class Health
{
    public event Action<GameObject> OnDeath = delegate { };
    public event Action<int> OnTakeDamage = delegate { };
    public event Action<int> OnRestoreHealth = delegate {  };

    private readonly int maxHealth;
    private readonly float invincibilityDuration;
    private int currentHealth;
    private int bonusMaxHealth;
    private float lastTimeTookDamage;
    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth + bonusMaxHealth;
    public bool IsDead => currentHealth <= 0;

    public Health(int maxHealth, float invincibilityDuration)
    {
        this.maxHealth = maxHealth;
        this.invincibilityDuration = invincibilityDuration;
    }

    public void Reset()
    {
        currentHealth = maxHealth;
        bonusMaxHealth = 0;
    }

    public void Restore(int amount)
    {
        currentHealth += amount;
        OnRestoreHealth(amount);
        if (currentHealth > MaxHealth)
            currentHealth = MaxHealth;
    }

    public void IncreaseMaxHealth(int amount)
    {
        bonusMaxHealth += amount;
        Restore(amount);
    }

    public void TakeDamage(GameObject source, int damage)
    {
        if(Time.time - lastTimeTookDamage < invincibilityDuration)
            return;

        currentHealth -= damage;
        OnTakeDamage(damage);
        if (IsDead)
        {
            OnDeath(source);
        }

        lastTimeTookDamage = Time.time;
    }
}