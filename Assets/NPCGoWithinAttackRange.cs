using UnityEngine;

public class NPCGoWithinAttackRange : IState
{
    private readonly NPCStateData npcStateData;
    private readonly EnemyNPC owner;
    private readonly NPCMoveData moveData;
    private readonly float attackRange;


    public NPCGoWithinAttackRange(NPCStateData npcStateData, EnemyNPC owner, NPCMoveData moveData, float attackRange)
    {
        this.npcStateData = npcStateData;
        this.owner = owner;
        this.moveData = moveData;
        this.attackRange = attackRange;
    }

    public void StateEnter()
    {

    }

    public void ListenToState()
    {
        if(Vector2.Distance(owner.Target.position, owner.transform.position) > attackRange)
        {
            var directionToPlayer = (owner.Target.position - owner.transform.position).normalized;
            owner.transform.Translate(moveData.MoveSpeed * Time.deltaTime * directionToPlayer);
        }
        else
        {
            npcStateData.ChangeState(NPCStates.Attack);
        }
    }

    public void StateExit()
    {
        
    }
}