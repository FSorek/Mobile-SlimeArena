using UnityEngine;

public class NPCAttackState : IState
{
    private readonly Transform attackOrigin;
    private readonly NPCAttackData attackData;
    private readonly Transform target;
    private double lastAttackTime;

    public NPCAttackState(Transform target, Transform attackOrigin, NPCAttackData attackData)
    {
        this.attackOrigin = attackOrigin;
        this.attackData = attackData;
        this.target = target;
    }
    public void StateEnter()
    {
        var projectile = ProjectilePool.Instance.Get().GetComponent<Projectile>();
        projectile.transform.position = attackOrigin.position;
        projectile.Shoot(target.position, attackData);
        projectile.gameObject.SetActive(true);
        lastAttackTime = Time.time;
    }

    public void ListenToState()
    {
        
    }
    
    public void StateExit()
    {
        
    }

    public bool CanAttack()
    {
        return Time.time - lastAttackTime >= attackData.ShootingRate;
    }
}