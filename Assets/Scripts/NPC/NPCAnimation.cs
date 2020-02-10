using System;
using UnityEngine;
[RequireComponent(typeof(EnemyNPC))]
public class NPCAnimation : EntityAnimator<EnemyNPC>
{
    protected override void Tick()
    {
        animator.SetBool("IsDead", owner.IsDead);
        
        animator.SetBool("IsMoving", owner.CurrentState == NPCStates.GoToCurrentTarget && !owner.IsDead);
        animator.SetBool("IsRepositioning", owner.CurrentState == NPCStates.Reposition && !owner.IsDead);
        renderer.flipX = (owner.transform.position.x - owner.Target.position.x) < 0;
    }
}
