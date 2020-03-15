using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour, ICanAttack
{
    public event Action OnSpawn = delegate { };
    private ITakeDamage healthComponent;
    public IAbilityPool AbilityPool { get; private set; }
    public Vector2 AttackDirection => PlayerInputManager.CurrentInput.AttackDirection;
    public Transform AttackOrigin => transform;

    private void Awake()
    {
        AbilityPool = new AbilityPool(40);
        healthComponent = GetComponent<ITakeDamage>();
        healthComponent.OnTakeDamage += OnTakeDamage;
    }

    private void OnTakeDamage(int amount)
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