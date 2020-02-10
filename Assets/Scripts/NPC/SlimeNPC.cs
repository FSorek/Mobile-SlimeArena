using UnityEngine;

public class SlimeNPC : EnemyNPC
{
    private RaycastHit2D[] lineOfSightItems = new RaycastHit2D[1];
    private LayerMask wallLayer;
    private double lastAttackTime;

    protected override void RegisterAllStates()
    {
        wallLayer = LayerMask.GetMask("World");

        StateMachine.CreateTransition(NPCStates.Idle, NPCStates.Reposition, AttackOffCooldown);
        StateMachine.CreateTransition(NPCStates.Idle, NPCStates.GoToCurrentTarget, () => !CanAttack());
        StateMachine.CreateTransition(NPCStates.GoToCurrentTarget, NPCStates.Idle, CanAttack);
        StateMachine.CreateTransition(NPCStates.Reposition, NPCStates.Attack, () => true);
        StateMachine.CreateTransition(NPCStates.Attack, NPCStates.Idle, () => true);
    }
    private bool AttackOffCooldown()
    {
        if (Time.fixedTime - lastAttackTime >= AttackData.ShootingRate)
        {
            lastAttackTime = Time.fixedTime;
            return true;
        }

        return false;
    }

    private bool CanAttack()
    {
        var distance = Vector2.Distance(Target.position, transform.position);
        if (distance > AttackRange)
            return false;
        
        var directionToPlayer = (Target.position - transform.position).normalized;
        Debug.DrawRay(transform.position, directionToPlayer * distance, Color.magenta);

        if (Physics2D.RaycastNonAlloc(transform.position, directionToPlayer, lineOfSightItems, distance,
                wallLayer) != 0)
            return false;
        
        return true;
    } 
}