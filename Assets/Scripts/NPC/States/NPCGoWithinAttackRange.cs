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
    private RaycastHit2D[] lineOfSightItems = new RaycastHit2D[1];
    private LayerMask wallLayer;
    
    public NPCGoWithinAttackRange(NPCStateData npcStateData, EnemyNPC owner, NPCMoveData moveData)
    {
        this.npcStateData = npcStateData;
        this.owner = owner;
        this.moveData = moveData;
        
        ownerSeeker = owner.GetComponent<Seeker>();
        lastPlayerPosition = owner.Target.position;
        ownerRBody = owner.GetComponent<Rigidbody2D>();
        wallLayer = LayerMask.GetMask("World");
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

        var directionToPlayer = (owner.Target.position + Vector3.up - owner.transform.position).normalized;
        var distance = Vector2.Distance(owner.Target.position + Vector3.up, owner.transform.position);
        Debug.DrawRay(owner.transform.position, directionToPlayer * distance, Color.magenta);
        if (distance < owner.AttackRange * .85f
            && Physics2D.RaycastNonAlloc(owner.transform.position, directionToPlayer, lineOfSightItems, distance, wallLayer) == 0)
        {
            npcStateData.ChangeState(NPCStates.Attack);
        }
        var moveDirection = ((Vector2)path.vectorPath[currentWaypoint] - ownerRBody.position).normalized;
        ownerRBody.MovePosition(ownerRBody.position + Time.fixedDeltaTime * moveData.MoveSpeed * moveDirection);
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
}