using UnityEngine;

public class CircularShot : AttackDecorator
{
    private readonly int numberOfProjectiles;

    public CircularShot(int numberOfProjectiles, Attack attack) : base(attack)
    {
        this.numberOfProjectiles = numberOfProjectiles;
    }

    public override void Create(Vector2 creationPosition, Vector2 direction)
    {
        for (int i = 0; i < numberOfProjectiles; i++)
        {
            float angle = i * Mathf.PI * 2 / numberOfProjectiles;
            Vector2 fireDirection = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;
            attack.Create(creationPosition, fireDirection);
        }
    }
}