using UnityEngine;

public class NPCAttack : IState
{
    private readonly Transform attackOrigin;
    private readonly NPCAttackData attackData;
    private readonly Transform target;
    private double lastAttackTime;

    public NPCAttack(Transform target, Transform attackOrigin, NPCAttackData attackData)
    {
        this.attackOrigin = attackOrigin;
        this.attackData = attackData;
        this.target = target;
    }
    public void StateEnter()
    {
        var projectile = ProjectilePool.Instance.Get().GetComponent<Projectile>();
        var attackPosition = attackOrigin.position;
        
        Vector2 directionToTarget = ((Vector2) target.position - (Vector2) attackPosition).normalized;
        Vector2 shootDirection = (directionToTarget + Random.insideUnitCircle * attackData.AccuracySpread).normalized;
        
        projectile.transform.position = attackPosition;
        projectile.Shoot(shootDirection, attackData.Damage);
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