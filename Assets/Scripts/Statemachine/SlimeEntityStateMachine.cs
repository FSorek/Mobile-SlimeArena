using System;
using UnityEngine;

[RequireComponent(typeof(EnemyNPC))]
[RequireComponent(typeof(NPCMover))]
[RequireComponent(typeof(NPCDodger))]
public class SlimeEntityStateMachine : MonoBehaviour, IEntityStateMachine
{
    public event Action<IState> OnEntityStateChanged = delegate {  };
    [SerializeField] private AttackData attackData;
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
            () => attack.CanAttack() && enemyNPC.HasLineOfSightTo(player.transform.position) && Vector2.Distance(transform.position, player.transform.position) <= attackData.MaxAttackRange);
        
        stateMachine.CreateTransition(
            attack,
            dodge,
            () => attack.HasCompletedAttack);
        
        stateMachine.CreateTransition(
            dodge,
            idle,
            () => !npcDodger.IsDodging);
        
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