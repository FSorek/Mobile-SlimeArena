using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyNPC : MonoBehaviour, ITakeDamage
{

    public event Action OnDeath = delegate { }; 
    [SerializeField] private NPCAttackData attackData;
    [SerializeField] private NPCMoveData moveData;
    [SerializeField] private int maxHealth;
    [SerializeField] private float timeUntilBodyIsGone = 2f;

    private readonly NPCStateData npcStateData = new NPCStateData();
    private StateMachine<NPCStates> stateMachine;
    private Collider2D activeCollider;
    private float attackRange;
    private Transform target;
    private Health health;

    public Transform Target => target;
    public float AttackRange => attackRange;
    //public bool IsMoving => !IsDead && npcStateData.CurrentState == NPCStates.GoWithinRange;
    public NPCStates CurrentState => npcStateData.CurrentState;
    public bool IsDead => health.CurrentHealth <= 0;


    private void Awake()
    {
        health = new Health(maxHealth);
        activeCollider = GetComponent<Collider2D>();
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
        health.Reset();
        npcStateData.ChangeState(NPCStates.GoWithinRange);
        activeCollider.enabled = true;
    }

    private void FixedUpdate()
    {
        if(IsDead)
            return;
        
        stateMachine.Tick();
    }

    public void TakeDamage(int damage)
    {
        health.TakeDamage(damage);
        if (IsDead)
        {
            activeCollider.enabled = false;
            Invoke(nameof(ReturnToPool), timeUntilBodyIsGone);
            OnDeath();
        }
    }

    private void ReturnToPool()
    {
        EnemyPool.Instance.ReturnToPool(this);
    }
}