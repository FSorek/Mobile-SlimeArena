﻿using System;
using UnityEngine;

public class NPCHealth : MonoBehaviour, ITakeDamage
{
    public event Action<NPCHealth> OnDeath = delegate { };
    
    [SerializeField] private int maxHealth = 1;
    private int currentHealth;
    private bool isDead;
    public bool IsDead => isDead;

    private void OnEnable()
    {
        currentHealth = maxHealth;
        isDead = false;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            OnDeath(this);
            Die();
        }
    }


    private void Die()
    {
        isDead = true;
        Debug.Log($"{gameObject.name} has died.");
    }
}