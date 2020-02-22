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
    
    private IPlayerInput playerInput;
    private PlayerAttack playerAttack;
    private PlayerAbility playerAbility;
    private PlayerScoreTracker playerScoreTracker;
    
    private IMovement initialMovement;
    private IMovement currentMovement;

    public PlayerScoreTracker PlayerScoreTracker => playerScoreTracker;
    public IPlayerInput PlayerInput => playerInput;
    public PlayerAttack PlayerAttack => playerAttack;
    public PlayerAbility PlayerAbility => playerAbility;
    public IMovement CurrentMovement => currentMovement;
    public Health Health { get; private set; }

    private void Awake()
    {
        Health = new Health(maxHealth);
        playerInput = new PlayerGamepadOrKeyboardInput();
        playerAttack = new PlayerAttack(this, attackData);
        playerAbility = new PlayerAbility(this, abilityData);
        playerScoreTracker = new PlayerScoreTracker();
        currentMovement = initialMovement = new InputMovement(this, moveSpeed);
        
        Health.OnTakeDamage += OnTakeDamage;
        Health.OnDeath += OnDeath;
    }

    private void OnDeath()
    {
        gameObject.SetActive(false);
    }

    private void OnTakeDamage(int damage)
    {
        if(playerAbility.IsUsingAbility)
            Health.RestoreHealth(damage);
    }

    private void OnEnable()
    {
        Health.Reset();
        playerAbility.Reset();
        OnSpawn();
    }
    private void Update()
    {
        playerInput.Tick();
        playerAbility.Tick();
        
    }
    private void FixedUpdate()
    {
        if(!playerAttack.IsAttacking)
            currentMovement.Move();
        else
            currentMovement.Move(attackData.SpeedPercentageOnAttack);
    }
    public void ChangeMovementStyle(IMovement movement)
    {
        currentMovement = movement;
        currentMovement.Initialize();
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
