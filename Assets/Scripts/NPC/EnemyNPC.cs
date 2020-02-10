using System;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class EnemyNPC : MonoBehaviour, ITakeDamage
{
    public event Action OnDeath = delegate { }; 
    [SerializeField] private NPCAttackData attackData;
    [SerializeField] private NPCRepositionAttackData repositionAttackData;
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

    protected NPCRepositionAttackData RepositionAttackData => repositionAttackData;
    protected NPCStateData NpcStateData => npcStateData;
    protected NPCAttackData AttackData => attackData;
    protected StateMachine<NPCStates> StateMachine => stateMachine;


    private void Awake()
    {
        health = new Health(maxHealth);
        activeCollider = GetComponent<Collider2D>();
        target = FindObjectOfType<Player>().transform;
        stateMachine = new StateMachine<NPCStates>(npcStateData);
        attackRange = Random.Range(attackData.MinAttackRange, attackData.MaxAttackRange);

        var npcIdleState = new NPCIdleState();
        var goToCurrentTargetState = new NPCGoToCurrentTargetState(this, RepositionAttackData);
        var npcAttackState = new NPCAttackState(AttackData, this);
        var npcRepositionState = new NPCRepositionState(this, RepositionAttackData);
        
        StateMachine.RegisterState(NPCStates.Idle, npcIdleState);
        StateMachine.RegisterState(NPCStates.GoToCurrentTarget, goToCurrentTargetState);
        StateMachine.RegisterState(NPCStates.Attack, npcAttackState);
        StateMachine.RegisterState(NPCStates.Reposition, npcRepositionState);

        RegisterAllStates();
    }

    private void Start()
    {
        npcStateData.ChangeState(NPCStates.Idle);
    }

    protected abstract void RegisterAllStates();

    private void OnEnable()
    {
        health.Reset();
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