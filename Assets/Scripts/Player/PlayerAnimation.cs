using System;
using UnityEngine;
[RequireComponent(typeof(Player))]

public class PlayerAnimation : EntityAnimator<Player>
{
    private Vector2 initialPosition;
    private Vector2 minVector = new Vector2(1,.25f).normalized;
    private Vector2 maxVector = new Vector2(-1,.25f).normalized;
    
    private void Start()
    {
        owner.PlayerAttack.OnAttack += PlayerOnAttack;
    }

    private void PlayerOnAttack(Vector2 direction)
    {
        var angle = Vector2.Angle(minVector, maxVector);
        var sumAngle = Vector2.Angle(minVector, direction) + Vector2.Angle(direction, maxVector);
        animator.SetTrigger(sumAngle - angle > 1f ?  "Attack" : "UpperAttack");
        
    }

    protected override void Tick()
    {
        animator.SetBool("IsAbilityActive", owner.PlayerAbility.IsUsingAbility);
        if(owner.PlayerAbility.IsUsingAbility)
            return;
        
        animator.SetBool("IsMoving", owner.CurrentMovement.IsMoving);
        if(owner.CurrentMovement.IsMoving)
            renderer.flipX = owner.PlayerInput.MoveVector.x < 0;
    }
}