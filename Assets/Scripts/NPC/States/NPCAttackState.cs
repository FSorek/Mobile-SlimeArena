using UnityEngine;

public class NPCAttackState : IState
{
    private readonly Transform attackOrigin;
    private readonly NPCAttackData attackData;
    private readonly Player player;
    private readonly LayerMask obstacleMask;
    private double lastAttackTime;

    public NPCAttackState(Player player, Transform attackOrigin, NPCAttackData attackData)
    {
        this.attackOrigin = attackOrigin;
        this.attackData = attackData;
        this.player = player;
        obstacleMask = LayerMask.GetMask("Player", "World");
    }
    public void StateEnter()
    {
        var projectile = ProjectilePool.Instance.Get().GetComponent<Projectile>();
        projectile.transform.position = attackOrigin.position;
        projectile.Shoot(player.transform, attackData);
        projectile.gameObject.SetActive(true);
        lastAttackTime = Time.time;
    }

    public void ListenToState()
    {
        
    }
    
    public void StateExit()
    {
        
    }
    
    public bool HasCleanAttackPath()
    {
        Vector2 attackPosition = attackOrigin.position;
        Vector2 playerPosition = player.transform.position;
        Vector2 directionToPlayer = (playerPosition - attackPosition).normalized;

        RaycastHit2D hit = Physics2D.Raycast(attackPosition, directionToPlayer, attackData.MaxAttackRange, obstacleMask);
        if (hit.collider != null)
            return hit.transform == player.transform;
        return false;
    }

    public bool CanAttack()
    {
        var canFireProjectile = Time.time - lastAttackTime >= attackData.ShootingRate;
        return HasCleanAttackPath() && canFireProjectile;
    }
}