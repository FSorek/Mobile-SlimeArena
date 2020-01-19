using Pathfinding;
using UnityEngine;

public class NPCGoWithinAttackRange : IState
{
    private const float DistanceToRecalculatePath = 3f;
    private const float WaypointStoppingDistance = 1f;
    
    private readonly NPCStateData npcStateData;
    private readonly EnemyNPC owner;
    private readonly NPCMoveData moveData;
    private readonly Seeker ownerSeeker;
    private readonly Rigidbody2D ownerRBody;

    private Vector3 lastPlayerPosition;
    private Path path;
    private int currentWaypoint;
    
    public NPCGoWithinAttackRange(NPCStateData npcStateData, EnemyNPC owner, NPCMoveData moveData)
    {
        this.npcStateData = npcStateData;
        this.owner = owner;
        this.moveData = moveData;
        
        ownerSeeker = owner.GetComponent<Seeker>();
        lastPlayerPosition = owner.Target.position;
        ownerRBody = owner.GetComponent<Rigidbody2D>();
    }

    public void StateEnter()
    {
        RecalculatePath();
    }

    public void ListenToState()
    {
        if(Vector2.Distance(owner.Target.position, lastPlayerPosition) > DistanceToRecalculatePath)
        {
            RecalculatePath();
            lastPlayerPosition = owner.Target.position;
        }
        
        if(path == null)
            return;

        var distance = Vector2.Distance(owner.Target.position, owner.transform.position);
        if (distance < owner.AttackRange * .85f)
        {
            npcStateData.ChangeState(NPCStates.Attack);
        }
        
        if (Vector2.Distance(owner.transform.position, path.vectorPath[currentWaypoint]) < WaypointStoppingDistance)
            currentWaypoint++;
        
        var moveDirection = ((Vector2)path.vectorPath[currentWaypoint] - ownerRBody.position).normalized;
        Debug.Log(moveDirection);
        ownerRBody.MovePosition(ownerRBody.position + Time.fixedDeltaTime * moveData.MoveSpeed * moveDirection);
    }

    private void RecalculatePath()
    {
        ownerSeeker.StartPath(owner.transform.position, owner.Target.position, PositionReached);
    }

    private void PositionReached(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    public void StateExit()
    {
        
    }
}