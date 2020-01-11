using UnityEngine;

public class NPCGoWithinAttackRange : IState
{
    private readonly NPCStateData npcStateData;
    private readonly EnemyNPC owner;
    private readonly NPCMoveData moveData;


    public NPCGoWithinAttackRange(NPCStateData npcStateData, EnemyNPC owner, NPCMoveData moveData)
    {
        this.npcStateData = npcStateData;
        this.owner = owner;
        this.moveData = moveData;
    }

    public void StateEnter()
    {
        
    }

    public void ListenToState()
    {
        if(Vector2.Distance(owner.Target.position, owner.transform.position) > owner.AttackRange * .9f)
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