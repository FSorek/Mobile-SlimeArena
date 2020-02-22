using System;
using UnityEngine;
[RequireComponent(typeof(EnemyNPC))]
public class NPCAnimation : EntityAnimator<EnemyNPC>
{
    private static readonly int IsDead = Animator.StringToHash("IsDead");
    private bool startsFlipped;

    private void Start()
    {
        startsFlipped = renderer.flipX;
        owner.Health.OnDeath += () => animator.SetBool(IsDead, true);
    }

    protected override void Tick()
    {
        //animator.SetBool("IsMoving", owner.CurrentState == NPCStates.GoToCurrentTarget && !owner.IsDead);
        //animator.SetBool("IsRepositioning", owner.CurrentState == NPCStates.Reposition && !owner.IsDead);
        
        bool shouldFlip = (owner.transform.position.x - owner.Target.position.x) < 0;
        renderer.flipX = startsFlipped ? !shouldFlip : shouldFlip;
    }
}
