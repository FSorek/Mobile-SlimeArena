using UnityEngine;

public class PowerAbsorber : AttackDecorator
{
    public PowerAbsorber(Attack attack) : base(attack)
    {
        attack.OnTargetHit += AttackOnTargetHit;
    }
    private void AttackOnTargetHit(ITakeDamage target)
    {
        var powerUpHolder = (target as MonoBehaviour)?.GetComponent<IPowerUpHolder>();
        if(target.CurrentHealth > 0 || powerUpHolder?.HeldPowerUp == null)
            return;

        if (Source is ICanAbsorb absorber)
            absorber.Absorb(powerUpHolder.HeldPowerUp);
        powerUpHolder.HeldPowerUp.Effect.ReturnToPool();
    }
}