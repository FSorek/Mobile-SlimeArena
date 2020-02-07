using System;
using UnityEngine;

public class Player : MonoBehaviour, ITakeDamage
{
    public event Action OnSpawn = delegate { };
    public event Action OnDeath = delegate { };
    [SerializeField] private int maxHealth = 1;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private PlayerAttackData attackData;
    [SerializeField] private PlayerAbilityData abilityData;
    [SerializeField] private Transform weaponSlot;
    
    private Health health;
    private PlayerInput playerInput;
    private PlayerAbility playerAbility;
    private PlayerAttack playerAttack;
    private ScoreTracker scoreTracker;
    
    private IMovement initialMovement;
    private IMovement currentMovement;

    public ScoreTracker ScoreTracker => scoreTracker;
    public PlayerInput PlayerInput => playerInput;
    public PlayerAbility PlayerAbility => playerAbility;
    public PlayerAttack PlayerAttack => playerAttack;
    public IMovement CurrentMovement => currentMovement;
    public Vector2 WeaponPosition => weaponSlot.position;
    public bool IsDead => health.CurrentHealth <= 0;
    private void Awake()
    {
        scoreTracker = new ScoreTracker();
        playerInput = new PlayerInput();
        playerAttack = new PlayerAttack(this, attackData);
        health = new Health(maxHealth);
        playerAbility = new PlayerAbility(this, abilityData);
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
        currentMovement.Move();
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
        if (IsDead && health.CurrentHealth + damage > 0)
        {
            OnDeath();
            gameObject.SetActive(false);
        }
    }
}
