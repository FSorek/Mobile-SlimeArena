﻿using System;
using UnityEngine;
[RequireComponent(typeof(IEnemyStateMachine))]
public class NPCAnimation : EntityAnimator<IEnemyStateMachine>
{
    private static readonly int IsDead = Animator.StringToHash("IsDead");
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private static readonly int IsRepositioning = Animator.StringToHash("IsRepositioning");
    private bool startsFlipped;
    private float lastPositionX;

    private void Start()
    {
        startsFlipped = renderer.flipX;
        owner.OnEnemyStateChanged += OwnerOnStateChanged;
    }

    private void OwnerOnStateChanged(IState state)
    {
        animator.SetBool(IsMoving, state is NPCGoToPlayer);
        animator.SetBool(IsRepositioning, state is NPCRepositionState);
        if(state is NPCDeadState)
            animator.SetTrigger(IsDead);
    }

    private void Update()
    {
        var currentPositionX = transform.position.x;
        if(lastPositionX == currentPositionX)
            return;
        bool shouldFlip = startsFlipped ? lastPositionX - currentPositionX > 0 : lastPositionX - currentPositionX < 0;
        if(renderer.flipX != shouldFlip)
            renderer.flipX = shouldFlip;
        lastPositionX = currentPositionX;
    }
}