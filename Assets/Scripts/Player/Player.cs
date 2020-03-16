using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour, ICanAttack
{
    public event Action OnSpawn = delegate { };
    private ITakeDamage healthComponent;
    private readonly IAbilityPool abilityPool = new AbilityPool(40);

    public IAbilityPool AbilityPool => abilityPool;

    public Vector2 AttackDirection => PlayerInputManager.CurrentInput.AttackDirection;
    public Transform AttackOrigin => transform;

    private void Awake()
    {
        healthComponent = GetComponent<ITakeDamage>();
        healthComponent.OnTakeDamage += TakeDamage;
    }

    private void TakeDamage(int amount)
    {
        if(healthComponent.CurrentHealth <= 0)
            Death();
    }

    private void Death()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        OnSpawn();
    }
}