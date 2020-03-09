using System;
using UnityEngine;

public abstract class Attack
{
    public GameObject Source { get; }

    public Attack(GameObject source)
    {
        Source = source;
    }
    public event Action<ITakeDamage> OnTargetHit = delegate {  }; 
    public abstract void Create(Vector2 creationPosition, Vector2 direction);

    protected void HitTarget(ITakeDamage target, int damage)
    {
        if(target == null)
            return;
        target.Health.TakeDamage(Source, damage);
        OnTargetHit(target);
    }
}