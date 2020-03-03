using System;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerEntityStateMachine : MonoBehaviour, IEntityStateMachine
{
    public event Action<IState> OnEntityStateChanged = delegate { };
    [SerializeField] private AttackData attackData;

    private StateMachine stateMachine = new StateMachine();
    private StateMachine movementStateMachine = new StateMachine();
    private Player player;

    public bool IsMoving => player.PlayerInput.MoveVector.magnitude > 0f;

    private void Awake()
    {
        player = GetComponent<Player>();
        
        var meleeAttack = new MeleeSlash(attackData.Damage, new Vector2(attackData.MinAttackRange, attackData.MaxAttackRange));
        var spinningAbility = new SpinningAbility(.2f, 2f);
        
        var idle = new EntityIdle();
        var attack = new EntityAttack(player, player.transform, meleeAttack, attackData);
        var ability = new CastingAbility(spinningAbility, player.AbilityPool);
        var dead = new EntityDead();

        stateMachine.OnStateChanged += (state) => OnEntityStateChanged(state);

        stateMachine.CreateTransition(
            idle,
            attack,
            () => player.PlayerInput.PrimaryActionDown
                  && attack.CanAttack());
        
        stateMachine.CreateTransition(
            attack,
            idle,
            () =>  attack.HasCompletedAttack);
        
        stateMachine.CreateTransition(
            idle,
            ability,
            () => player.PlayerInput.SecondaryActionDown && ability.CanCast);
        
        stateMachine.CreateTransition(
            ability,
            idle,
            () => player.PlayerInput.SecondaryActionUp);
        
        stateMachine.CreateAnyTransition(
            dead,
            () => player.Health.IsDead);

        stateMachine.SetState(idle);
        
        var inputMovement = new InputMovement(player, 5f);
        movementStateMachine.SetState(inputMovement);
    }

    private void Update()
    {
        stateMachine.Tick();
    }

    private void FixedUpdate()
    {
        if(IsMoving && !(GameStateMachine.CurrentGameState is GameBossCinematic))
            movementStateMachine.Tick();
    }
}