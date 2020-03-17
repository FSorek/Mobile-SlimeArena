using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, ITakeDamage, ICanRestore
{
    public event Action<int> OnRestore = delegate {  };
    public event Action<int> OnTakeDamage = delegate {  };
    [SerializeField] private int maxHealth;
    [SerializeField] private float invincibilityDuration;
    private float lastTimeTookDamage;
    private IEntityStateMachine playerStateMachine;
    private bool isInvincible;
    public int CurrentHealth { get; private set; }
    public int MaxHealth => maxHealth;

    private void Awake()
    {
        CurrentHealth = maxHealth;
        playerStateMachine = GetComponent<IEntityStateMachine>();
        playerStateMachine.OnEntityStateChanged += (state) => isInvincible = state is EntityCastingAbility;
    }

    public void Restore(int amount)
    {
        CurrentHealth += amount;
        OnRestore(amount);
        if (CurrentHealth > maxHealth)
            CurrentHealth = maxHealth;
    }
    public void TakeDamage(ICanAttack source, int damage)
    {
        if(GameSceneStateMachine.CurrentGameState is GameBossCinematic
           || isInvincible
           || Time.time - lastTimeTookDamage < invincibilityDuration)
            return;

        CurrentHealth -= damage;
        OnTakeDamage(damage);
        lastTimeTookDamage = Time.time;
    }
}