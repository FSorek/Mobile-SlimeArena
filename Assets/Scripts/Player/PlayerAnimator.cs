using System;
using UnityEngine;
[RequireComponent(typeof(Player))]

public class PlayerAnimator : EntityAnimator<IEntityStateMachine>
{
    private void Start()
    {
        owner.OnEntityStateChanged += EntityStateChanged;
    }

    private void EntityStateChanged(IState state)
    {
        
    }
}
//var angle = Vector2.Angle(minVector, maxVector);
//var sumAngle = Vector2.Angle(minVector, direction) + Vector2.Angle(direction, maxVector);
//animator.SetTrigger(sumAngle - angle > 1f ?  "Attack" : "UpperAttack");