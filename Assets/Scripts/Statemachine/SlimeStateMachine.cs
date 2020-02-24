﻿using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyNPC))]
[RequireComponent(typeof(NPCMover))]
[RequireComponent(typeof(NPCDodger))]
public class SlimeStateMachine : MonoBehaviour, IEnemyStateMachine
{
    public event Action<IState> OnEnemyStateChanged = delegate {  };
    [SerializeField] private NPCAttackData attackData;
    private StateMachine stateMachine = new StateMachine();
    private void Awake()
    {
        var npcMover = GetComponent<NPCMover>();
        var npcDodger = GetComponent<NPCDodger>();
        var enemyNPC = GetComponent<EnemyNPC>();
        var player = FindObjectOfType<Player>();
        
        var idle = new NPCIdleState();
        var goToPlayer = new NPCGoToPlayer(player, npcMover);
        var attack = new NPCAttackState(player, enemyNPC.AttackOrigin, attackData);
        var dodge = new NPCRepositionState(npcDodger);
        var dead = new NPCDeadState();

        stateMachine.OnStateChanged += (state) => OnEnemyStateChanged(state);
        
        stateMachine.CreateTransition(
            idle,
            goToPlayer,
            () => !attack.HasCleanAttackPath() || Vector2.Distance(transform.position, player.transform.position) > attackData.MaxAttackRange);
        
        stateMachine.CreateTransition(
            goToPlayer,
            idle,
            () => attack.HasCleanAttackPath() && Vector2.Distance(transform.position, player.transform.position) <= attackData.MinAttackRange);
        
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
            idle,
            () => !npcDodger.IsDodging);
        
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