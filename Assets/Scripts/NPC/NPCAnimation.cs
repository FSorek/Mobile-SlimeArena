using UnityEngine;
[RequireComponent(typeof(EnemyNPC))]
public class NPCAnimation : EntityAnimator<IEnemyStateMachine>
{
    private static readonly int IsDead = Animator.StringToHash("IsDead");
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private static readonly int IsRepositioning = Animator.StringToHash("IsRepositioning");
    private bool startsFlipped;

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
}