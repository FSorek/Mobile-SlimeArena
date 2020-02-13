using UnityEngine;

public class NPCRepositionState : IState
{
    private readonly EnemyNPC owner;
    private readonly NPCRepositionAttackData repositionAttackData;
    private readonly Rigidbody2D ownerRb;

    private Vector3 repositionDirection;
    private float totalDistance;
    private bool canExit;

    public NPCRepositionState(EnemyNPC owner, NPCRepositionAttackData repositionAttackData)
    {
        this.owner = owner;
        this.repositionAttackData = repositionAttackData;
        ownerRb = owner.GetComponent<Rigidbody2D>();
    }
    public void StateEnter()
    {
        var targetDirection = owner.Target.position - owner.transform.position;
        repositionDirection = Vector3.Cross(targetDirection, Vector3.forward).normalized;
        if (Random.value > 0.5f)
            repositionDirection *= -1;
        totalDistance = 0f;
        canExit = false;
    }

    public void ListenToState()
    {
        if(canExit) return;
        
        var positionThisFrame = repositionAttackData.RepositionSpeed * Time.fixedDeltaTime * (Vector2)repositionDirection;
        ownerRb.MovePosition(ownerRb.position + positionThisFrame);
        totalDistance += positionThisFrame.magnitude;
        if (totalDistance >= repositionAttackData.RepositionDistance && !canExit)
            canExit = true;
    }

    public void StateExit()
    {
        
    }

    public bool CanExit => canExit;
}

