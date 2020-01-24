using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyNPC : MonoBehaviour, ITakeDamage
{
    public event Action OnDeath = delegate { }; 
    [SerializeField] private NPCAttackData attackData;
    [SerializeField] private NPCMoveData moveData;
    [SerializeField] private int maxHealth;

    private Health health;
    private StateMachine<NPCStates> stateMachine;
    private readonly NPCStateData npcStateData = new NPCStateData();
    private Transform target;
    private float attackRange;

    public Transform Target => target;
    public float AttackRange => attackRange;

    public Health Health => health;

    public bool IsMoving => npcStateData.CurrentState == NPCStates.GoWithinRange;
    public bool IsRepositioning => npcStateData.CurrentState == NPCStates.RepositionAttack;

    private void Awake()
    {
        target = FindObjectOfType<Player>().transform;
        stateMachine = new StateMachine<NPCStates>(npcStateData);
        attackRange = Random.Range(attackData.MinAttackRange, attackData.MaxAttackRange);
        
        var npcGoToPositionState = new NPCGoWithinAttackRange(npcStateData, this, moveData);
        var npcAttackState = new NPCAttackState(attackData, this, npcStateData);
        var npcRepositionAttackState = new NPCRepositionAttackState(this, moveData, npcStateData);
        
        stateMachine.RegisterState(NPCStates.GoWithinRange, npcGoToPositionState);
        stateMachine.RegisterState(NPCStates.Attack, npcAttackState);
        stateMachine.RegisterState(NPCStates.RepositionAttack, npcRepositionAttackState);
    }

    private void OnEnable()
    {
        health = new Health(maxHealth);
        npcStateData.ChangeState(NPCStates.GoWithinRange);
    }

    private void FixedUpdate()
    {
        stateMachine.Tick();
    }

    public void TakeDamage(int damage)
    {
        health.TakeDamage(damage);
        if (IsDead && health.CurrentHealth + damage > 0)
            OnDeath();
    }

    public bool IsDead => health.CurrentHealth <= 0;
}