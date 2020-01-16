using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, ITakeDamage
{
    [SerializeField] private int maxHealth = 1;
    private int currentHealth;
    private bool isDead;

    public bool IsDead => isDead;
    private void Awake()
    {
        currentHealth = maxHealth;
        isDead = false;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
            Die();
    }


    private void Die()
    {
        isDead = true;
        Debug.Log($"Player has died.");
    }
}