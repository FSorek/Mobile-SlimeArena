using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour, ITakeDamage
{
    public event Action OnSpawn = delegate { };
    [SerializeField] private int maxHealth = 1;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private PlayerAttackData attackData;
    [SerializeField] private PlayerAbilityData abilityData;

    private IMovement initialMovement;

    public IPlayerInput PlayerInput { get; private set; }
    public PlayerAttack PlayerAttack { get; private set; }
    public PlayerAbility PlayerAbility { get; private set; }
    public IMovement CurrentMovement { get; private set; }
    public Health Health { get; private set; }

    private void Awake()
    {
        Health = new Health(maxHealth);
        PlayerInput = new PlayerGamepadOrKeyboardInput();
        PlayerAttack = new PlayerAttack(this, attackData);
        PlayerAbility = new PlayerAbility(this, abilityData);
        CurrentMovement = initialMovement = new InputMovement(this, moveSpeed);
        
        Health.OnTakeDamage += OnTakeDamage;
        Health.OnDeath += OnDeath;
    }

    private void OnDeath()
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
    private void FixedUpdate()
    {
        if(!PlayerAttack.IsAttacking)
            CurrentMovement.Move();
        else
            CurrentMovement.Move(attackData.SpeedPercentageOnAttack);
    }
    public void ChangeMovementStyle(IMovement movement)
    {
        CurrentMovement = movement;
        CurrentMovement.Initialize();
    }
    public void ResetMovementStyle()
    {
        ChangeMovementStyle(initialMovement);
    }

    private void OnDrawGizmosSelected()
    {
        var attackDirection = PlayerInput == null ? new Vector2(0, 0) : PlayerInput.MoveVector; 
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube((Vector2)transform.position + attackDirection, attackData.AttackSize);
    }
}
