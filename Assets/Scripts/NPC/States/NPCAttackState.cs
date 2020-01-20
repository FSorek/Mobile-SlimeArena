using UnityEngine;

public class NPCAttackState : IState
{
    private readonly NPCAttackData attackData;
    private readonly EnemyNPC owner;
    private readonly NPCStateData stateData;

    private float lastAttackTime;
    private RaycastHit2D[] lineOfSightItems = new RaycastHit2D[1];
    private LayerMask wallLayer;
    
    public NPCAttackState(NPCAttackData attackData, EnemyNPC owner, NPCStateData stateData)
    {
        this.owner = owner;
        this.stateData = stateData;
        this.attackData = attackData;
        lastAttackTime = 0f;
        wallLayer = LayerMask.GetMask("World");
    }
    public void StateEnter()
    {
        if(!CanAttack()) 
            return;
        
        var projectile = ProjectilePool.Instance.Get();
        projectile.transform.position = owner.transform.position;
        projectile.Shoot(owner.Target, attackData);
        projectile.gameObject.SetActive(true);
        lastAttackTime = Time.time;
    }

    public void ListenToState()
    {
        
        if (CanAttack())
        {
            stateData.ChangeState(NPCStates.RepositionAttack);
        }
        else
        {
            stateData.ChangeState(NPCStates.GoWithinRange);
        }
    }

    private bool CanAttack()
    {
        if (Vector2.Distance(owner.transform.position, owner.Target.position) > owner.AttackRange)
            return false;
        
        var directionToPlayer = (owner.Target.position - owner.transform.position).normalized;
        var distance = Vector2.Distance(owner.Target.position, owner.transform.position);
        if (Physics2D.RaycastNonAlloc(owner.transform.position, directionToPlayer, lineOfSightItems, distance,
                wallLayer) != 0)
            return false;
        
        return Time.fixedTime - lastAttackTime >= attackData.ShootingRate;
    } 

    public void StateExit()
    {
        
    }
}