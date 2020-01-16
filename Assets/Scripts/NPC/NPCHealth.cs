using System;
using UnityEngine;

public class NPCHealth : MonoBehaviour, ITakeDamage
{
    public event Action<NPCHealth> OnDeath = delegate { };
    
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
        {
            OnDeath(this);
            Die();
        }
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name} has died.");
    }
}