using UnityEngine;

public class BossAnimator : EntityAnimator<BossStateMachine>
{
    private static readonly int Die = Animator.StringToHash("Die");
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");
    private static readonly int Attack = Animator.StringToHash("Attack");

    private void Start()
    {
        owner.OnEnemyStateChanged += OwnerOnStateChanged;
    }

    private void OwnerOnStateChanged(IState state)
    {
        animator.SetBool(IsMoving, state is NPCGoToPlayer);
        if(state is NPCDead)
            animator.SetTrigger(Die);
        if(state is NPCAttack || state is NPCAttackInCircle)
            animator.SetTrigger(Attack);
    }
}
