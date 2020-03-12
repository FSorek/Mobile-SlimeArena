using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour, ITakeDamage, ICanAttack
{
    public event Action OnSpawn = delegate { };
    [SerializeField] private int maxHealth = 1;
    public Health Health { get; private set; }
    public IAbilityPool AbilityPool { get; private set; }

    public Vector2 AttackDirection => PlayerInputManager.CurrentInput.AttackDirection;

    private void Awake()
    {
        Health = new Health(maxHealth, .5f);
        Health.OnDeath += Death;
        AbilityPool = new AbilityPool(40);
    }

    private void Death(GameObject source)
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Health.Reset();
        OnSpawn();
    }

    private void Update()
    {
        if(GameSceneStateMachine.CurrentGameState is GameBossCinematic)
            return;
    }
}