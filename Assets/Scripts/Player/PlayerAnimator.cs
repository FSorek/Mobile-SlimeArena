using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerAnimator : EntityAnimator<PlayerEntityStateMachine>
{
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private static readonly int CastAbility = Animator.StringToHash("CastAbility");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int UpperAttack = Animator.StringToHash("UpperAttack");
    private IState currentState;

    private void Start()
    {
        owner.OnEntityStateChanged += OwnerOnStateChanged;
    }

    private void OwnerOnStateChanged(IState state)
    {
        if (state is EntityAttack)
            animator.SetTrigger(Random.value <= .5f ? Attack : UpperAttack);
        if(state is CastingAbility)
            animator.SetTrigger(CastAbility);
        currentState = state;
    }

    private void Update()
    {
        animator.SetBool(IsMoving, owner.IsMoving && (currentState is EntityIdle || currentState is null));
    }
}