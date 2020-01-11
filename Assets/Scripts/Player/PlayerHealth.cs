using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, ITakeDamage
{
    [SerializeField] private int maxHealth = 1;
    private int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        Debug.Log($"Player has died.");
    }
}