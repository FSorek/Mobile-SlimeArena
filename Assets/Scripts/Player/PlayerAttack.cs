using System;
using UnityEngine;

public class PlayerAttack
{
    private readonly Player owner;
    private readonly PlayerAttackData attackData;
    public event Action<ITakeDamage> OnTargetHit = delegate { };
    public event Action<Vector2> OnAttack = delegate { };
    
    private RaycastHit2D[] targetsHit = new RaycastHit2D[20];
    private float lastAttackTime;
    private LayerMask enemyLayer;
    private Vector2 lastDirection;
    public Vector2 LastDirection => lastDirection;

    public PlayerAttack(Player owner, PlayerAttackData attackData)
    {
        this.owner = owner;
        this.attackData = attackData;
        enemyLayer = LayerMask.GetMask("NPC");
    }
    public void Attack(Vector2 direction)
    {
        lastDirection = direction;
        if(Time.time - lastAttackTime < attackData.AttackDelay)
            return;
        var boxCastAngle = Vector2.SignedAngle(Vector2.up, direction);
        var resultAmount = Physics2D.BoxCastNonAlloc(owner.WeaponPosition, attackData.AttackSize, boxCastAngle, direction, targetsHit, 2f, enemyLayer);

        for (int i = 0; i < resultAmount; i++)
        {
            if(targetsHit[i].transform == owner.transform)
                continue;
            var target = targetsHit[i].transform.GetComponent<ITakeDamage>();
            HitTarget(target, attackData.Damage);
        }
        OnAttack(direction);
        lastAttackTime = Time.time;
    }

    public void HitTarget(ITakeDamage target, int damage)
    {
        if(target == null) 
            return;
        target.TakeDamage(damage);
        OnTargetHit(target);
        if (target.IsDead)
        {
            owner.ScoreTracker.AddScore();
        }
    }
}