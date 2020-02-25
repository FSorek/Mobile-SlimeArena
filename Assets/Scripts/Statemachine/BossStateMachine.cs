using System;
using System.Net.NetworkInformation;
using UnityEngine;

[RequireComponent(typeof(EnemyNPC))]
[RequireComponent(typeof(NPCMover))]
public class BossStateMachine : MonoBehaviour, IStateMachine
{
    public event Action<IState> OnEnemyStateChanged = delegate {  };

    [SerializeField] private NPCAttackInCircleData attackData;
    [SerializeField] private NPCSequenceData sequenceData;
    [Range(0f, 1f)]
    [SerializeField] private float secondStageHealthThreshold = .5f;
    
    private readonly StateMachine stateMachine = new StateMachine();

    private void Awake()
    {
        var player = FindObjectOfType<Player>();
        var npcMover = GetComponent<NPCMover>();
        var enemyNPC = GetComponent<EnemyNPC>();
        
        var idle = new NPCIdle();
        var goToPlayer = new NPCGoToPlayer(player, npcMover);
        var attack = new NPCAttackInCircle(enemyNPC.AttackOrigin, attackData);
        var sequenceAttack = new NPCSequence(sequenceData);
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
            () => attack.CanAttack() && enemyNPC.HasLineOfSightTo(player.transform.position) && Vector2.Distance(transform.position, player.transform.position) <= attackData.MaxAttackRange);
        
        stateMachine.CreateTransition(
            attack,
            idle,
            () => enemyNPC.Health.CurrentHealth >= enemyNPC.Health.MaxHealth * secondStageHealthThreshold);
        
        stateMachine.CreateTransition(
            attack,
            sequenceAttack,
            () => enemyNPC.Health.CurrentHealth < enemyNPC.Health.MaxHealth * secondStageHealthThreshold);
        
        stateMachine.CreateTransition(
            sequenceAttack,
            attack,
            () => sequenceAttack.CanContinueSequence
            );
        
        stateMachine.CreateTransition(
            sequenceAttack,
            idle,
            () => !sequenceAttack.CanContinueSequence
        );
        
        stateMachine.CreateAnyTransition(
            dead,
            () => enemyNPC.Health.IsDead);
        
        stateMachine.SetState(idle);
    }

    private void Update()
    {
        if(GameStateMachine.CurrentGameState is GameBossFight)
            stateMachine.Tick();
    }
}