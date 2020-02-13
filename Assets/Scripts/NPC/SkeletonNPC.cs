using UnityEngine;

public class SkeletonNPC : EnemyNPC
{
    [SerializeField] private int projectilesPerAttack;
    [SerializeField] private float delayBetweenSequenceAttacks;

    private int attackCounter;
    protected override void CreateTransitions()
    {
        StateMachine.CreateTransition(NPCStates.GoToCurrentTarget, NPCStates.Idle, HasCleanAttackPath);
        StateMachine.CreateTransition(NPCStates.Idle, NPCStates.GoToCurrentTarget, () => !HasCleanAttackPath());
        StateMachine.CreateTransition(NPCStates.Idle, NPCStates.Reposition, () => StateMachine.TimeSinceStateExit(NPCStates.Attack) >= AttackData.ShootingRate && HasCleanAttackPath(), () => attackCounter = 0);
        
        StateMachine.CreateTransition(NPCStates.Reposition, NPCStates.Idle, () => attackCounter >= projectilesPerAttack);
        StateMachine.CreateTransition(NPCStates.Reposition, NPCStates.Attack, () => StateMachine.TimeSinceStateExit(NPCStates.Attack) >= delayBetweenSequenceAttacks &&  attackCounter < projectilesPerAttack, () => attackCounter++);
        StateMachine.CreateTransition(NPCStates.Attack, NPCStates.Reposition, () => true);
    }

}