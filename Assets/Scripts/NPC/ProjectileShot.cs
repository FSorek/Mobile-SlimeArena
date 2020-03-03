using UnityEngine;

public class ProjectileShot : Attack
{
    private readonly int damage;

    public ProjectileShot(int damage)
    {
        this.damage = damage;
    }

    public override void Create(Vector2 creationPosition, Vector2 direction)
    {
        var projectile = ProjectilePool.Instance.Get().GetComponent<Projectile>();
        var attackPosition = creationPosition;

        projectile.transform.position = attackPosition;
        projectile.Shoot(direction, HitTarget);
        projectile.gameObject.SetActive(true);
    }

    private void HitTarget(ITakeDamage target)
    {
        HitTarget(target,damage);
    }
}