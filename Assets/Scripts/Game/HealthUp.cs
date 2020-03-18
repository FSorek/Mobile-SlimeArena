using System.Collections.Generic;
using UnityEngine;

public class HealthUp : IPowerUp
{
    private const int Amount = 1;

    public GameObject Effect { get; set; }

    public void Carry(GameObject carrier)
    {
        var restorableEntity = carrier.GetComponent<ICanRestore>();
        restorableEntity?.Restore(Amount);
    }

    public void Apply(GameObject entity)
    {
        var restorableEntity = entity.GetComponent<ICanRestore>();
        restorableEntity?.Restore(Amount);
    }
}