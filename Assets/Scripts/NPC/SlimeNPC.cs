public class SlimeNPC : EnemyNPC
{
    protected override void CreateTransitions()
    {
        StateMachine.CreateTransition(NPCStates.Idle, NPCStates.Reposition, () => StateMachine.TimeSinceStateExit(NPCStates.Attack) >= AttackData.ShootingRate && HasCleanAttackPath());
        StateMachine.CreateTransition(NPCStates.Idle, NPCStates.GoToCurrentTarget, () => !HasCleanAttackPath());
        StateMachine.CreateTransition(NPCStates.GoToCurrentTarget, NPCStates.Idle, HasCleanAttackPath);
        StateMachine.CreateTransition(NPCStates.Reposition, NPCStates.Attack, () => true);
        StateMachine.CreateTransition(NPCStates.Attack, NPCStates.Idle, () => true);
    }
}