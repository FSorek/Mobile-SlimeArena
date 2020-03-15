using UnityEngine;

public interface ICanAttack
{
    Vector2 AttackDirection { get; }
    Transform AttackOrigin { get; }
}