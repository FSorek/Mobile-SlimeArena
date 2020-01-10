using UnityEngine;

public class NPCAttackState : IState
{
    private readonly NPCAttackData attackData;
    private readonly EnemyNPC owner;
    private readonly Transform target;
    private readonly NPCStateData stateData;
    private readonly float attackRange;

    public NPCAttackState(NPCAttackData attackData, EnemyNPC owner, Transform target, NPCStateData stateData, float attackRange)
    {
        this.attackData = attackData;
        this.owner = owner;
        this.target = target;
        this.stateData = stateData;
        this.attackRange = attackRange;
    }
    public void StateEnter()
    {
        var projectile = GameObject.Instantiate(attackData.ProjectileModel, owner.transform.position, Quaternion.identity)
            .AddComponent<Projectile>();;
        projectile.SetTarget(target);
    }

    public void ListenToState()
    {
        if(Vector2.Distance(owner.transform.position, target.position) > attackRange)
            stateData.ChangeState(NPCStates.GoToPosition);
        else
            stateData.ChangeState(NPCStates.RepositionAttack);
    }

    public void StateExit()
    {
    }
}

public class Projectile : MonoBehaviour
{
    public void SetTarget(Transform target)
    {
        
    }
}