using System;
using UnityEngine;

public class SkeletonEntityStateMachine : MonoBehaviour, IEntityStateMachine
{
    public event Action<IState> OnEntityStateChanged = delegate {  };
    
    [SerializeField] private AttackData attackData;
    [SerializeField] private SequenceData sequenceData;

    private StateMachine stateMachine = new StateMachine();


    private void Awake()
    {
        var npcMover = GetComponent<NPCMover>();
        var npcDodger = GetComponent<NPCDodger>();
        var enemyNPC = GetComponent<EnemyNPC>();
        var npcHealth = GetComponent<ITakeDamage>();
        var player = FindObjectOfType<Player>();

        var projectile = new ProjectileShot(attackData.Damage, enemyNPC);
        
        var idle = new EntityIdle();
        var goToPlayer = new NPCGoToPlayer(player, npcMover);
        var attack = new EntityAttack(enemyNPC, enemyNPC.AttackOrigin, projectile, attackData);
        var dodge = new NPCDodge(npcDodger);
        var attackSequenceState = new EntitySequence(sequenceData);
        var dead = new EntityDead();
        
        stateMachine.OnStateChanged += (state) => OnEntityStateChanged(state);
        
        stateMachine.CreateTransition(
            idle,
            goToPlayer,
            () => !enemyNPC.HasLineOfSightTo(player.transform.position) || Vector2.Distance(transform.position, player.transform.position) > attackData.MaxAttackRange);
        
        stateMachine.CreateTransition(
            goToPlayer,
            idle,
            () => enemyNPC.HasLineOfSightTo(player.transform.position) && Vector2.Distance(transform.position, player.transform.position) <= attackData.MinAttackRange);
        
        stateMachine.CreateTransition(
            idle,
            attack,
            () => attack.CanAttack() && Vector2.Distance(transform.position, player.transform.position) <= attackData.MaxAttackRange);
        
        stateMachine.CreateTransition(
            attack,
            dodge,
            () => attack.HasCompletedAttack);

        stateMachine.CreateTransition(
            dodge,
            attackSequenceState,
            () => !npcDodger.IsDodging);
        
        stateMachine.CreateTransition(
            attackSequenceState,
            attack,
            () => attackSequenceState.CanContinueSequence);
        
        stateMachine.CreateTransition(
            attackSequenceState,
            idle,
            () => !attackSequenceState.CanContinueSequence);
        
        stateMachine.CreateAnyTransition(
            dead,
            () => npcHealth.CurrentHealth <= 0);
        
        stateMachine.CreateTransition(
            dead,
            idle,
            () => npcHealth.CurrentHealth > 0);

        stateMachine.SetState(idle);
    }

    private void Update()
    {
        stateMachine.Tick();
    }
}