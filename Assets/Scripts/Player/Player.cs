using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour, ITakeDamage, ICanAttack
{
    public event Action OnSpawn = delegate { };
    [SerializeField] private int maxHealth = 1;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private PlayerAbilityData abilityData;

    public IPlayerInput PlayerInput { get; private set; }
    public PlayerAbility PlayerAbility { get; private set; }
    public Health Health { get; private set; }

    public Vector2 AttackDirection => PlayerInput.AttackDirection;

    private void Awake()
    {
        Health = new Health(maxHealth, .5f);
        PlayerAbility = new PlayerAbility(this, abilityData);
        PlayerInput = new PlayerGamepadOrKeyboardInput();
        Health.OnTakeDamage += OnTakeDamage;
        Health.OnDeath += Death;
    }

    private void Death()
    {
        gameObject.SetActive(false);
    }

    private void OnTakeDamage(int damage)
    {
        if(PlayerAbility.IsUsingAbility)
            Health.Restore(damage);
    }

    private void OnEnable()
    {
        Health.Reset();
        PlayerAbility.Reset();
        OnSpawn();
    }

    private void Update()
    {
        if(GameStateMachine.CurrentGameState is GameBossCinematic)
            return;
        PlayerInput.Tick();
        PlayerAbility.Tick();
    }
}