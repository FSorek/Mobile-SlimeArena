using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyNPC : MonoBehaviour, ITakeDamage
{
    [SerializeField] private NPCAttackData attackData;
    [SerializeField] private NPCMoveData moveData;
    [SerializeField] private int maxHealth = 1;
    private int currentHealth;
    private StateMachine<NPCStates> stateMachine;
    private NPCStateData npcStateData = new NPCStateData();
    private Player player;
    private float attackRange;

    public Transform Target => player.transform;
    public float AttackRange => attackRange;
    public bool IsMoving => npcStateData.CurrentState == NPCStates.GoWithinRange;
    public bool IsRepositioning => npcStateData.CurrentState == NPCStates.RepositionAttack;

    private void Awake()
    {
        currentHealth = maxHealth;
        stateMachine = new StateMachine<NPCStates>(npcStateData);
        player = FindObjectOfType<Player>();
        attackRange = Random.Range(attackData.MinAttackRange, attackData.MaxAttackRange);
        
        var npcGoToPositionState = new NPCGoWithinAttackRange(npcStateData, this, moveData);
        var npcAttackState = new NPCAttackState(attackData, this, npcStateData);
        var npcRepositionAttackState = new NPCRepositionAttackState(this, moveData, npcStateData);
        
        stateMachine.RegisterState(NPCStates.GoWithinRange, npcGoToPositionState);
        stateMachine.RegisterState(NPCStates.Attack, npcAttackState);
        stateMachine.RegisterState(NPCStates.RepositionAttack, npcRepositionAttackState);
    }

    private void Update()
    {
        stateMachine.Tick();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    private void Die()
    {
        Debug.Log($"{gameObject.name} has died.");
    }
}