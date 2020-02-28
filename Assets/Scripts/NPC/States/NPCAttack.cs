using UnityEngine;

public class NPCAttack : IState
{
    private readonly Transform target;
    private readonly Transform attackOrigin;
    private readonly Attack attack;
    private readonly AttackData attackData;
    private float attackCastingTimer;
    private float lastAttackTime;

    public bool HasCompletedAttack { get; private set; }

    public NPCAttack(Transform target, Transform attackOrigin, Attack attack, AttackData attackData)
    {
        this.target = target;
        this.attackOrigin = attackOrigin;
        this.attack = attack;
        this.attackData = attackData;
    }
    public void StateEnter()
    {
        attackCastingTimer = attackData.CastingTime;
    }

    public void ListenToState()
    {
        attackCastingTimer -= Time.deltaTime;
        if(attackCastingTimer <= 0 && !HasCompletedAttack)
        {
            Vector2 directionToTarget = ((Vector2) target.position - (Vector2) attackOrigin.position).normalized;
            attack.Create(attackOrigin.position, directionToTarget);
            HasCompletedAttack = true;
            lastAttackTime = Time.time;
        }
    }

    public void StateExit()
    {
        HasCompletedAttack = false;
    }

    public bool CanAttack()
    {
        return Time.time - lastAttackTime >= attackData.ShootingRate;
    }
}