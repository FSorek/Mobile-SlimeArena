using UnityEngine;

public class PlayerHealth : MonoBehaviour, ITakeDamage
{
    [SerializeField]private int currentHealth = 1;
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