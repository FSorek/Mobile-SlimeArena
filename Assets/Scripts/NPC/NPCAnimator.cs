using System;
using UnityEngine;
[RequireComponent(typeof(IStateMachine))]
public class NPCAnimator : EntityAnimator<IStateMachine>
{
    private static readonly int IsDead = Animator.StringToHash("IsDead");
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private static readonly int IsRepositioning = Animator.StringToHash("IsRepositioning");

    private void Start()
    {
        owner.OnEnemyStateChanged += OwnerOnStateChanged;
    }

    private void OwnerOnStateChanged(IState state)
    {
        animator.SetBool(IsMoving, state is NPCGoToPlayer);
        animator.SetBool(IsRepositioning, state is NPCDodge);
        if(state is NPCDead)
            animator.SetTrigger(IsDead);
    }
}