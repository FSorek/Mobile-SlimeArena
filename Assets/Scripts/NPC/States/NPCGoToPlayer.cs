using Pathfinding;
using UnityEngine;

public class NPCGoToPlayer : IState
{
    private const float PlayerMovedUpdateThreshold = 1f;
    private readonly NPCMover mover;
    private readonly Player player;

    private Vector3 lastPlayerPosition;
    private Path path;
    private int currentWaypoint;

    public NPCGoToPlayer(Player player, NPCMover mover)
    {
        this.mover = mover;
        this.player = player;
    }

    public void StateEnter()
    {
        OrderMove();
    }

    public void ListenToState()
    {
        if (Vector3.Distance(lastPlayerPosition, player.transform.position) > PlayerMovedUpdateThreshold)
            OrderMove();
    }

    private void OrderMove()
    {
        var playerPosition = player.transform.position;
        mover.MoveTo(playerPosition);
        lastPlayerPosition = playerPosition;
    }
    
    public void StateExit()
    {
        mover.Stop();
    }
}