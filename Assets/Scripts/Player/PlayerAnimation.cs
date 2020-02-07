using System;
using UnityEngine;
[RequireComponent(typeof(Player))]

public class PlayerAnimation : EntityAnimator<Player>
{
    [SerializeField] private Transform hands;
    private Vector2 initialPosition;

    private void Start()
    {
        owner.PlayerAttack.OnAttack += PlayerOnAttack;
        initialPosition = hands.transform.localPosition;
    }

    private void PlayerOnAttack(Vector2 direction)
    {
        animator.SetTrigger("Attack");
    }

    protected override void Tick()
    {
        hands.rotation = Quaternion.FromToRotation(Vector2.up, owner.PlayerAttack.LastDirection);
        hands.localPosition = initialPosition + owner.PlayerAttack.LastDirection;
        
        animator.SetBool("IsAbilityActive", owner.PlayerAbility.IsUsingAbility);
        if(owner.PlayerAbility.IsUsingAbility)
            return;
        
        animator.SetBool("IsMoving", owner.CurrentMovement.IsMoving);
        renderer.flipX = owner.PlayerInput.MoveVector.x < 0;
    }
}