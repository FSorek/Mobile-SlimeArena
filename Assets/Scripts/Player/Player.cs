using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour, ITakeDamage
{
    public event Action OnDeath = delegate { };
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private int maxHealth = 1;
    [SerializeField] private PlayerAttackData attackData;
    [SerializeField] private PlayerAbilityData abilityData;
    [SerializeField] private Transform weaponSlot;
    
    private PlayerInput playerInput;
    private Health health;
    private PlayerAbility playerAbility;
    private PlayerAttack playerAttack;
    
    private IMovement initialMovement;
    private IMovement currentMovement;

    public PlayerInput PlayerInput => playerInput;
    public PlayerAbility PlayerAbility => playerAbility;
    public Health Health => health;
    public PlayerAttack PlayerAttack => playerAttack;
    public IMovement CurrentMovement => currentMovement;
    public Vector2 WeaponPosition => weaponSlot.position;

    private void Awake()
    {
        playerInput = new PlayerInput();
        playerAttack = new PlayerAttack(this, attackData);
        health = new Health(maxHealth);
        playerAbility = new PlayerAbility(this, abilityData);
        currentMovement = initialMovement = new InputMovement(this, moveSpeed);
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
            OnDeath();
    }

    public bool IsDead => health.CurrentHealth <= 0;
}

//to-do:
        
//make touch events fire for 2+ touches :)
//abstract some of this logic and create a static attack joystick :)
//keep attack logic the same, allow all directions of attack :)

//turn the sword into a dagger so that stabbing makes more sense :)

//make ability have animation :)
//make the ability have a time pool :)
//refill time pool with kills outside of ability :)

//create the arena :)
//adjust npc logic for collisions :)
//adjust layers and collisions :)

//enemy keeps moving if direct raycast hits a wall :)
//stop velocity after using ability :)

//create enemy spawners :)
//add enemy deaths and pooling :)
//add player deaths

//consider destroying projectiles

//add kill count
//add a simple menu
