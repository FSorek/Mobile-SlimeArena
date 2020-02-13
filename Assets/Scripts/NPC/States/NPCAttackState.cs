using UnityEngine;

public class NPCAttackState : IState
{
    private readonly NPCAttackData attackData;
    private readonly EnemyNPC owner;

    
    public NPCAttackState(EnemyNPC owner, NPCAttackData attackData)
    {
        this.owner = owner;
        this.attackData = attackData;
    }
    public void StateEnter()
    {
        var projectile = ProjectilePool.Instance.Get().GetComponent<Projectile>();
        projectile.transform.position = (Vector2)owner.transform.position + attackData.ShootPositionOffset;
        projectile.Shoot(owner.Target, attackData);
        projectile.gameObject.SetActive(true);
    }

    public void ListenToState()
    {

    }


    public void StateExit()
    {
        
    }

    public bool CanExit => true;
}