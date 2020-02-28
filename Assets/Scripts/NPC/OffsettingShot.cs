using UnityEngine;

public class OffsettingShot : AttackDecorator
{
    private readonly int offsetDegree;
    private int lastOffset;

    public OffsettingShot(int offsetDegree, Attack attack) : base(attack)
    {
        this.offsetDegree = offsetDegree;
    }

    public override void Create(Vector2 creationPosition, Vector2 direction)
    {
        direction = Quaternion.Euler(0, 0, lastOffset + offsetDegree) * direction;
        lastOffset += offsetDegree;
        attack.Create(creationPosition, direction);
    }
}