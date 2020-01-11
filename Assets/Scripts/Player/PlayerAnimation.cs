using UnityEngine;
[RequireComponent(typeof(Player))]
public class PlayerAnimation : EntityAnimator<Player>
{
    protected override void Tick()
    {
        animator.SetBool("IsMoving", owner.IsMoving);
        spriteRenderer.flipX = owner.PlayerInput.MoveVector.x < 0;
    }
}