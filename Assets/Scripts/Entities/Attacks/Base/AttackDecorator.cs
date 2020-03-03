using UnityEngine;

public abstract class AttackDecorator : Attack
{
    protected Attack attack;

    public AttackDecorator(Attack attack)
    {
        this.attack = attack;
    }
    public override void Create(Vector2 creationPosition, Vector2 direction)
    {
        attack.Create(creationPosition, direction);
    }
}