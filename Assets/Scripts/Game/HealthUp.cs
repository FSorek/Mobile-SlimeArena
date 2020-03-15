using UnityEngine;

public class HealthUp : IPowerUp
{
    private const int Amount = 1;

    public void PickUpPower(GameObject entity)
    {
        var restorableEntity = entity.GetComponent<ICanRestore>();
        restorableEntity?.Restore(Amount);
    }

    public void Absorb(GameObject entity)
    {
        var restorableEntity = entity.GetComponent<ICanRestore>();
        restorableEntity?.Restore(Amount);
    }

    public ParticleSystem ParticleSystem { get; set; }
}