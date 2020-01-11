using UnityEngine;

public class NPCRepositionAttackState : IState
{
    private readonly EnemyNPC owner;
    private readonly NPCMoveData moveData;
    private readonly NPCStateData stateData;

    private Vector3 repositionDirection;
    private float totalDistance;

    public NPCRepositionAttackState(EnemyNPC owner, NPCMoveData moveData, NPCStateData stateData)
    {
        this.owner = owner;
        this.moveData = moveData;
        this.stateData = stateData;
    }
    public void StateEnter()
    {
        var targetDirection = owner.Target.position - owner.transform.position;
        repositionDirection = Vector3.Cross(targetDirection, Vector3.forward).normalized;
        if (Random.value > 0.5f)
            repositionDirection *= -1;
        totalDistance = 0f;
    }

    public void ListenToState()
    {
        var distanceThisFrame = moveData.MoveSpeed * Time.deltaTime * repositionDirection;
        owner.transform.Translate(distanceThisFrame);
        totalDistance += distanceThisFrame.magnitude;
        if (totalDistance >= moveData.RepositionDistance)
            stateData.ChangeState(NPCStates.Attack);
    }

    public void StateExit()
    {
        
    }
    
    
}