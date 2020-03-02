using UnityEngine;

public class BossAnimator : EntityAnimator<BossEntityStateMachine>
{
    private static readonly int Die = Animator.StringToHash("Die");
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private static readonly int Attack = Animator.StringToHash("Attack");

    private void Start()
    {
        owner.OnEntityStateChanged += OwnerOnStateChanged;
    }

    private void OwnerOnStateChanged(IState state)
    {
        animator.SetBool(IsMoving, state is NPCGoToPlayer);
        if(state is EntityDead)
            animator.SetTrigger(Die);
        if(state is EntityAttack)
            animator.SetTrigger(Attack);
    }
}
