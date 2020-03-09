using UnityEngine;

public class EntityAttack : IState
{
    private readonly ICanAttack owner;
    private readonly Transform attackOrigin;
    private readonly Attack attack;
    private readonly AttackData attackData;
    private float attackCastingTimer;
    private float lastAttackTime;

    public bool HasCompletedAttack { get; private set; }

    public EntityAttack(ICanAttack owner, Transform attackOrigin, Attack attack, AttackData attackData)
    {
        this.owner = owner;
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
            attack.Create(attackOrigin.position, owner.AttackDirection);
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