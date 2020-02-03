using System;
using UnityEngine;

public class Health
{
    private readonly int maxHealth;
    
    private int currentHealth;
    public int CurrentHealth => currentHealth;

    public Health(int maxHealth)
    {
        this.maxHealth = maxHealth;
    }
    public void Reset()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }
}