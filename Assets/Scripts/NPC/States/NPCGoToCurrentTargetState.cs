using Pathfinding;
using UnityEngine;

public class NPCGoToCurrentTargetState : IState
{
    private const float DistanceToRecalculatePath = 3f;
    private const float WaypointStoppingDistance = 1f;
    
    private readonly EnemyNPC owner;
    private readonly NPCRepositionAttackData repositionAttackData;
    private readonly Seeker ownerSeeker;
    private readonly Rigidbody2D ownerRBody;

    private Vector3 lastPlayerPosition;
    private Path path;
    private int currentWaypoint;

    public NPCGoToCurrentTargetState(EnemyNPC owner, NPCRepositionAttackData repositionAttackData)
    {
        this.owner = owner;
        this.repositionAttackData = repositionAttackData;
        
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

        if(path.vectorPath.Count <= currentWaypoint)
            return;
        var moveDirection = ((Vector2)path.vectorPath[currentWaypoint] - ownerRBody.position).normalized;
        ownerRBody.MovePosition(ownerRBody.position + Time.fixedDeltaTime * repositionAttackData.MoveSpeed * moveDirection);
        if (Vector2.Distance(owner.transform.position, path.vectorPath[currentWaypoint]) < WaypointStoppingDistance)
            currentWaypoint++;
        
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

    public bool CanExit => true;
}