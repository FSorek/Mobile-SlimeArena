using System;
using UnityEngine;

public class SkeletonStateMachine : MonoBehaviour, IStateMachine
{
    public event Action<IState> OnEnemyStateChanged = delegate {  };
    
    [SerializeField] private NPCAttackData attackData;
    [SerializeField] private NPCSequenceData sequenceData;

    private StateMachine stateMachine = new StateMachine();


    private void Awake()
    {
        var npcMover = GetComponent<NPCMover>();
        var npcDodger = GetComponent<NPCDodger>();
        var enemyNPC = GetComponent<EnemyNPC>();
        var player = FindObjectOfType<Player>();
        
        var idle = new NPCIdle();
        var goToPlayer = new NPCGoToPlayer(player, npcMover);
        var attack = new NPCAttack(player.transform, enemyNPC.AttackOrigin, attackData);
        var dodge = new NPCDodge(npcDodger);
        var attackSequenceState = new NPCSequence(sequenceData);
        var dead = new NPCDead();
        
        stateMachine.OnStateChanged += (state) => OnEnemyStateChanged(state);
        
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
            () => true);

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
            () => enemyNPC.Health.IsDead);

        stateMachine.SetState(idle);
    }

    private void Update()
    {
        stateMachine.Tick();
    }
}