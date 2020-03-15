using UnityEngine;

public class PoolRestoring : AttackDecorator
{
    private readonly int amount;
    private readonly IAbilityPool pool;
    public PoolRestoring(int amount, IAbilityPool pool, Attack attack) : base(attack)
    {
        this.amount = amount;
        this.pool = pool;
        attack.OnTargetHit += AttackOnTargetHit;
    }

    private void AttackOnTargetHit(ITakeDamage target)
    {
        if(target.CurrentHealth <= 0)
            pool.Restore(amount);
    }
}