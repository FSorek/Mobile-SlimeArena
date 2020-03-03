using System;
using UnityEngine;
[RequireComponent(typeof(IEntityStateMachine))]
public class NPCAnimator : EntityAnimator<IEntityStateMachine>
{
    private static readonly int IsDead = Animator.StringToHash("IsDead");
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private static readonly int IsRepositioning = Animator.StringToHash("IsRepositioning");

    private void Start()
    {
        owner.OnEntityStateChanged += OwnerOnStateChanged;
    }

    private void OwnerOnStateChanged(IState state)
    {
        animator.SetBool(IsMoving, state is NPCGoToPlayer);
        animator.SetBool(IsRepositioning, state is NPCDodge);
        if(state is EntityDead)
            animator.SetTrigger(IsDead);
    }
}