﻿using System;
using UnityEngine;

[RequireComponent(typeof(EnemyNPC))]
[RequireComponent(typeof(NPCMover))]
[RequireComponent(typeof(NPCDodger))]
public class SlimeStateMachine : MonoBehaviour, IStateMachine
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
        
        var idle = new NPCIdle();
        var goToPlayer = new NPCGoToPlayer(player, npcMover);
        var attack = new NPCAttack(player.transform, enemyNPC.AttackOrigin, attackData);
        var dodge = new NPCDodge(npcDodger);
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