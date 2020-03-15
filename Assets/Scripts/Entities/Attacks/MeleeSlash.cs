using UnityEngine;

public class MeleeSlash : Attack
{
    private readonly int damage;
    private readonly Vector2 size;
    private readonly LayerMask enemyLayer;
    private Collider2D[] targetsHit = new Collider2D[10];

    public MeleeSlash(int damage, Vector2 size, ICanAttack source) : base(source)
    {
        this.damage = damage;
        this.size = size;
        enemyLayer = LayerMask.GetMask("NPC");
    }
    public override void Create(Vector2 creationPosition, Vector2 direction)
    {
        var resultAmount = Physics2D.OverlapBoxNonAlloc(creationPosition + direction,
            size, 0, targetsHit, enemyLayer);
        for (int i = 0; i < resultAmount; i++)
        {
            HitTarget(targetsHit[i].GetComponent<ITakeDamage>(), damage);
            ReflectTarget(targetsHit[i].GetComponent<IReflectable>());
        }
    }

    private void ReflectTarget(IReflectable reflected)
    {
        reflected?.Reflect();
    }
}