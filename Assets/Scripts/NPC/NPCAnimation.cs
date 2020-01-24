using System;
using UnityEngine;
[RequireComponent(typeof(EnemyNPC))]
public class NPCAnimation : EntityAnimator<EnemyNPC>
{
    protected override void Tick()
    {
        animator.SetBool("IsMoving", owner.IsMoving);
        animator.SetBool("IsRepositioning", owner.IsRepositioning);
        renderer.flipX = (owner.transform.position.x - owner.Target.position.x) < 0;
    }
}
