using UnityEngine;

public class HealthUp : IPowerUp
{
    private const int Amount = 1;

    public void PickUpPower(ITakeDamage entity)
    {
        entity.Health.IncreaseMaxHealth(Amount);
    }

    public void Absorb(ITakeDamage entity)
    {
        entity.Health.Restore(Amount);
    }
}