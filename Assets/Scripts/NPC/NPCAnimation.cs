using System;
using UnityEngine;
[RequireComponent(typeof(EnemyNPC))]
public class NPCAnimation : EntityAnimator<EnemyNPC>
{
    private bool startsFlipped;

    private void Start()
    {
        startsFlipped = renderer.flipX;
    }

    protected override void Tick()
    {
        animator.SetBool("IsDead", owner.IsDead);
        
        animator.SetBool("IsMoving", owner.CurrentState == NPCStates.GoToCurrentTarget && !owner.IsDead);
        animator.SetBool("IsRepositioning", owner.CurrentState == NPCStates.Reposition && !owner.IsDead);
        
        bool shouldFlip = (owner.transform.position.x - owner.Target.position.x) < 0;
        renderer.flipX = startsFlipped ? !shouldFlip : shouldFlip;
    }
}
