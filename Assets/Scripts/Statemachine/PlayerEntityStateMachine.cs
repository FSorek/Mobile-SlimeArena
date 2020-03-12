using System;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerEntityStateMachine : MonoBehaviour, IEntityStateMachine
{
    public event Action<IState> OnEntityStateChanged = delegate { };
    [SerializeField] private AttackData attackData;
    [SerializeField] private float moveSpeed = 5f;

    private StateMachine stateMachine = new StateMachine();
    private StateMachine movementStateMachine = new StateMachine();
    private Player player;

    public bool IsMoving => PlayerInputManager.CurrentInput.MoveVector.magnitude > 0f;

    private void Awake()
    {
        player = GetComponent<Player>();
        
        var meleeSlash = new MeleeSlash(attackData.Damage, new Vector2(attackData.MinAttackRange, attackData.MaxAttackRange), gameObject);
        var playerAttack = new PoolRestoring(2, player.AbilityPool, meleeSlash);
        
        var spinningAbility = new SpinningAbility(player.transform, .1f, 1, 1, new Vector2(3,3));
        
        var idle = new EntityIdle();
        var attack = new EntityAttack(player, player.transform, playerAttack, attackData);
        var ability = new EntityCastingAbility(spinningAbility, player.AbilityPool);
        var dead = new EntityDead();

        stateMachine.OnStateChanged += (state) => OnEntityStateChanged(state);

        stateMachine.CreateTransition(
            idle,
            attack,
            () => PlayerInputManager.CurrentInput.PrimaryActionDown
                  && attack.CanAttack());
        
        stateMachine.CreateTransition(
            attack,
            idle,
            () =>  attack.HasCompletedAttack);
        
        stateMachine.CreateTransition(
            idle,
            ability,
            () => PlayerInputManager.CurrentInput.SecondaryActionDown && ability.CanCast);
        
        stateMachine.CreateTransition(
            ability,
            idle,
            () => PlayerInputManager.CurrentInput.SecondaryActionUp || !ability.CanCast);
        
        stateMachine.CreateAnyTransition(
            dead,
            () => player.Health.IsDead);

        stateMachine.SetState(idle);
        
        var inputMovement = new InputMovement(player, moveSpeed);
        movementStateMachine.SetState(inputMovement);
    }

    private void Start()
    {
        player.Health.OnTakeDamage += PlayerOnTakeDamage;
    }

    private void PlayerOnTakeDamage(int amount) // a tiny hack to make player invincible during ability casting
    {
        if(stateMachine.CurrentState is EntityCastingAbility)
            player.Health.Restore(amount);
    }

    private void Update()
    {
        stateMachine.Tick();
    }

    private void FixedUpdate()
    {
        if(IsMoving && !(GameSceneStateMachine.CurrentGameState is GameBossCinematic))
            movementStateMachine.Tick();
    }
}