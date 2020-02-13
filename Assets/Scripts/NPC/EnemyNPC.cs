using System;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class EnemyNPC : MonoBehaviour, ITakeDamage, IGameObjectPooled
{
    public event Action OnDeath = delegate { };
    public event Action OnTakeDamage = delegate {  };
    [SerializeField] private int maxHealth;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float timeUntilBodyIsGone = 2f;
    [SerializeField] private NPCAttackData attackData;
    [SerializeField] private NPCRepositionAttackData repositionData;

    private RaycastHit2D[] lineOfSightItems = new RaycastHit2D[1];
    private readonly NPCStateData npcStateData = new NPCStateData();
    private StateMachine<NPCStates> stateMachine;
    private Collider2D activeCollider;
    private float attackRange;
    private Transform target;
    private Health health;
    private int wallLayer;

    public Transform Target => target;
    public NPCStates CurrentState => npcStateData.CurrentState;
    public bool IsDead => health.CurrentHealth <= 0;

    protected StateMachine<NPCStates> StateMachine => stateMachine;

    protected float MoveSpeed => moveSpeed;
    protected NPCAttackData AttackData => attackData;

    private void Awake()
    {
        health = new Health(maxHealth);
        activeCollider = GetComponent<Collider2D>();
        target = FindObjectOfType<Player>().transform;
        stateMachine = new StateMachine<NPCStates>(npcStateData);
        attackRange = Random.Range(attackData.MinAttackRange, attackData.MaxAttackRange);
        wallLayer = LayerMask.GetMask("World");
        
        var npcIdleState = new NPCIdleState();
        var goToCurrentTargetState = new NPCGoToCurrentTargetState(this, MoveSpeed);
        var npcAttackState = new NPCAttackState(this, AttackData);
        var npcRepositionState = new NPCRepositionState(this, repositionData);
        
        StateMachine.RegisterState(NPCStates.Idle, npcIdleState);
        StateMachine.RegisterState(NPCStates.GoToCurrentTarget, goToCurrentTargetState);
        StateMachine.RegisterState(NPCStates.Attack, npcAttackState);
        StateMachine.RegisterState(NPCStates.Reposition, npcRepositionState);
        CreateTransitions();
    }

    private void Start()
    {
        npcStateData.ChangeState(NPCStates.Idle);
    }

    protected abstract void CreateTransitions();

    protected virtual void OnEnable()
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
        OnTakeDamage();
        if (IsDead)
        {
            activeCollider.enabled = false;
            Invoke(nameof(ReturnToPool), timeUntilBodyIsGone);
            OnDeath();
        }
    }

    private void ReturnToPool()
    {
        gameObject.ReturnToPool();
    }

    protected bool HasCleanAttackPath()
    {
        var position = transform.position;
        var distance = Vector2.Distance(position, Target.position);
        var directionToPlayer = (Target.position - position).normalized;
        return distance < attackRange && Physics2D.RaycastNonAlloc(position, directionToPlayer, lineOfSightItems, distance,
                   wallLayer) == 0;
    }

    public ObjectPool Pool { get; set; }
}