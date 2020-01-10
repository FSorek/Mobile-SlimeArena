using System;
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

    public Transform Target => player.transform;
    private void Awake()
    {
        currentHealth = maxHealth;
        stateMachine = new StateMachine<NPCStates>(npcStateData);
        player = FindObjectOfType<Player>();
        var attackRange = Random.Range(attackData.MinAttackRange, attackData.MaxAttackRange);
        var npcGoToPositionState = new NPCGoWithinAttackRange(npcStateData, this, moveData, attackRange);
        var npcAttackState = new NPCAttackState(attackData, this, player.transform, npcStateData, attackRange);
        stateMachine.RegisterState(NPCStates.GoToPosition, npcGoToPositionState);
        stateMachine.RegisterState(NPCStates.Attack, npcAttackState);
        
        
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