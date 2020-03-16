using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, ITakeDamage
{
    public event Action<int> OnTakeDamage = delegate {  };
    [SerializeField] private int maxHealth;
    [SerializeField] private float invincibilityDuration;
    private float lastTimeTookDamage;
    private int maxHealthBonus;
    public int CurrentHealth { get; private set; }
    public int MaxHealth => maxHealth + maxHealthBonus;

    private void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(ICanAttack source, int damage)
    {
        if(Time.time - lastTimeTookDamage < invincibilityDuration)
            return;

        CurrentHealth -= damage;
        OnTakeDamage(damage);
        lastTimeTookDamage = Time.time;
    }
}