using System;
using UnityEngine;

public class Player : MonoBehaviour, ITakeDamage
{
    public event Action OnSpawn = delegate { };
    public event Action OnDeath = delegate { };
    public event Action OnTakeDamage = delegate {  };
    [SerializeField] private int maxHealth = 1;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private PlayerAttackData attackData;
    [SerializeField] private PlayerAbilityData abilityData;
    
    private Health health;
    private PlayerInput playerInput;
    private PlayerAttack playerAttack;
    private PlayerAbility playerAbility;
    private PlayerScoreTracker playerScoreTracker;
    
    private IMovement initialMovement;
    private IMovement currentMovement;

    public PlayerScoreTracker PlayerScoreTracker => playerScoreTracker;
    public PlayerInput PlayerInput => playerInput;
    public PlayerAttack PlayerAttack => playerAttack;
    public PlayerAbility PlayerAbility => playerAbility;
    public IMovement CurrentMovement => currentMovement;
    public bool IsDead => health.CurrentHealth <= 0;
    public int CurrentHealth => health.CurrentHealth;
    private void Awake()
    {
        health = new Health(maxHealth);
        playerInput = new PlayerInput();
        playerAttack = new PlayerAttack(this, attackData);
        playerAbility = new PlayerAbility(this, abilityData);
        playerScoreTracker = new PlayerScoreTracker();
        currentMovement = initialMovement = new InputMovement(this, moveSpeed);
    }
    private void OnEnable()
    {
        health.Reset();
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
    public void TakeDamage(int damage)
    {
        if(playerAbility.IsUsingAbility)
            return;
        
        health.TakeDamage(damage);
        OnTakeDamage();
        if (IsDead && health.CurrentHealth + damage > 0)
        {
            OnDeath();
            gameObject.SetActive(false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        var attackDirection = PlayerInput == null ? new Vector2(0, 0) : PlayerInput.MoveVector; 
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube((Vector2)transform.position + attackDirection, attackData.AttackSize);
    }
}
