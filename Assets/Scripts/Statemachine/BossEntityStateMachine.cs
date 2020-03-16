using System;
using System.Net.NetworkInformation;
using UnityEngine;

[RequireComponent(typeof(EnemyNPC))]
[RequireComponent(typeof(NPCMover))]
public class BossEntityStateMachine : MonoBehaviour, IEntityStateMachine
{
    public event Action<IState> OnEntityStateChanged = delegate {  };

    [SerializeField] private AttackData attackData;
    [Range(0f, 1f)]
    [SerializeField] private float secondStageHealthThreshold = .5f;
    
    private readonly StateMachine stateMachine = new StateMachine();

    private void Awake()
    {
        var player = FindObjectOfType<Player>();
        var npcMover = GetComponent<NPCMover>();
        var enemyNPC = GetComponent<EnemyNPC>();
        var npcHealth = GetComponent<ITakeDamage>();
        
        var projectileShot = new ProjectileShot(attackData.Damage, enemyNPC);
        var circleShot = new OffsettingShot(5, new CircularShot(12, projectileShot));
        
        var idle = new EntityIdle();
        var goToPlayer = new NPCGoToPlayer(player, npcMover);
        var firstStageAttack = new EntityAttack(enemyNPC, enemyNPC.AttackOrigin, projectileShot, attackData);
        var secondStageAttack = new EntityAttack(enemyNPC, enemyNPC.AttackOrigin, circleShot, attackData);
        var dead = new EntityDead();

        stateMachine.OnStateChanged += (state) => OnEntityStateChanged(state);
        
        stateMachine.CreateTransition(
            idle,
            goToPlayer,
            () => !enemyNPC.HasLineOfSightTo(player.transform.position) 
                  || Vector2.Distance(transform.position, player.transform.position) > attackData.MaxAttackRange);
        
        stateMachine.CreateTransition(
            goToPlayer,
            idle,
            () => enemyNPC.HasLineOfSightTo(player.transform.position) 
                  && Vector2.Distance(transform.position, player.transform.position) <= attackData.MinAttackRange);
        
        stateMachine.CreateTransition(
            idle,
            firstStageAttack,
            () => firstStageAttack.CanAttack() 
                  && npcHealth.CurrentHealth >= npcHealth.MaxHealth * secondStageHealthThreshold
                  && enemyNPC.HasLineOfSightTo(player.transform.position) 
                  && Vector2.Distance(transform.position, player.transform.position) <= attackData.MaxAttackRange);
        
        stateMachine.CreateTransition(
            idle,
            secondStageAttack,
            () => firstStageAttack.CanAttack() 
                  && npcHealth.CurrentHealth < npcHealth.MaxHealth * secondStageHealthThreshold
                  && enemyNPC.HasLineOfSightTo(player.transform.position) 
                  && Vector2.Distance(transform.position, player.transform.position) <= attackData.MaxAttackRange);
        
        stateMachine.CreateTransition(
            secondStageAttack,
            firstStageAttack,
            () => secondStageAttack.HasCompletedAttack);
        
        stateMachine.CreateTransition(
            firstStageAttack,
            idle,
            () => firstStageAttack.HasCompletedAttack);
        
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
        if(GameSceneStateMachine.CurrentGameState is GameBossFight)
            stateMachine.Tick();
    }
}