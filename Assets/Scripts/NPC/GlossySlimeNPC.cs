using UnityEngine;

public class GlossySlimeNPC : EnemyNPC
{
    protected override void RegisterAllStates()
    {
        StateMachine.CreateTransition(NPCStates.Idle, NPCStates.GoToCurrentTarget, () => !CanChase());
        StateMachine.CreateTransition(NPCStates.GoToCurrentTarget, NPCStates.Idle, CanChase);
    }

    private bool CanChase()
    {
        var distance = Vector2.Distance(Target.position, transform.position);
        if (distance > AttackRange)
            return false;
        return true;
    }
}