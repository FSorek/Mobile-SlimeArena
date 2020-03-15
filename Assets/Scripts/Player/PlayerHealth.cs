using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, ITakeDamage, ICanRestore
{
    public event Action<int> OnRestore = delegate {  };
    public event Action<int> OnTakeDamage = delegate {  };
    [SerializeField] private int maxHealth;
    [SerializeField] private float invincibilityDuration;
    private float lastTimeTookDamage;
    public int CurrentHealth { get; private set; }
    public int MaxHealth => maxHealth;

    public void Restore(int amount)
    {
        CurrentHealth += amount;
        OnRestore(amount);
        if (CurrentHealth > MaxHealth)
            CurrentHealth = MaxHealth;
    }
    public void TakeDamage(ICanAttack source, int damage)
    {
        if(GameSceneStateMachine.CurrentGameState is GameBossCinematic 
           || Time.time - lastTimeTookDamage < invincibilityDuration)
            return;

        CurrentHealth -= damage;
        OnTakeDamage(damage);
        lastTimeTookDamage = Time.time;
    }
}