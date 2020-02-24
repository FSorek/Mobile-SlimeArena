using UnityEngine;

public class NPCAttackInCircleState : IState
{
    private readonly Transform attackOrigin;
    private readonly NPCAttackData attackData;
    private readonly int numberOfProjectiles;
    private float lastAttackTime;

    public NPCAttackInCircleState(Transform attackOrigin, NPCAttackData attackData, int numberOfProjectiles)
    {
        this.attackOrigin = attackOrigin;
        this.attackData = attackData;
        this.numberOfProjectiles = numberOfProjectiles;
    }
    public void StateEnter()
    {
        for (int i = 0; i < numberOfProjectiles; i++)
        {
            float angle = i * Mathf.PI * 2 / numberOfProjectiles;
            Vector2 pos = (Vector2)attackOrigin.position + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            
            var projectile = ProjectilePool.Instance.Get().GetComponent<Projectile>();
            projectile.transform.position = attackOrigin.position;
            projectile.Shoot(pos, attackData);
            projectile.gameObject.SetActive(true);
        }
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