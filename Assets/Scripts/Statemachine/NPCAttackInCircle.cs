using UnityEngine;

public class NPCAttackInCircle : IState
{
    private readonly Transform attackOrigin;
    private readonly NPCAttackInCircleData attackData;
    private float lastAttackTime;
    private int attackOffset;

    public NPCAttackInCircle(Transform attackOrigin, NPCAttackInCircleData attackData)
    {
        this.attackOrigin = attackOrigin;
        this.attackData = attackData;
    }
    public void StateEnter()
    {
        for (int i = 0; i < attackData.NumberOfProjectiles; i++)
        {
            float angle = attackOffset + i * Mathf.PI * 2 / attackData.NumberOfProjectiles;
            Vector2 fireDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;
            
            var projectile = ProjectilePool.Instance.Get().GetComponent<Projectile>();
            projectile.transform.position = attackOrigin.position;
            projectile.Shoot(fireDirection, attackData.Damage);
            projectile.gameObject.SetActive(true);
        }
    }

    public void ListenToState()
    {
    }

    public void StateExit()
    {
        attackOffset += attackData.NextAttackOFfset;
        lastAttackTime = Time.time;
    }
    
    public bool CanAttack()
    {
        return Time.time - lastAttackTime >= attackData.ShootingRate;
    }
}