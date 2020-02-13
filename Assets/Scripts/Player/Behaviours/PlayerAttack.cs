﻿using System;
using UnityEngine;

public class PlayerAttack
{
    private readonly Player owner;
    private readonly PlayerAttackData attackData;
    public event Action<ITakeDamage> OnTargetHit = delegate { };
    public event Action<Vector2> OnAttack = delegate { };
    
    private Collider2D[] targetsHit = new Collider2D[10];
    private float lastAttackTime;
    private LayerMask enemyLayer;
    private Collider2D col;
    public bool IsAttacking => Time.time - lastAttackTime < attackData.AttackRootDuration;


    public PlayerAttack(Player owner, PlayerAttackData attackData)
    {
        this.owner = owner;
        this.attackData = attackData;
        enemyLayer = LayerMask.GetMask("NPC");
    }
    public void Attack(Vector2 direction)
    {
        if(Time.time - lastAttackTime < attackData.AttackDelay)
            return;
        //var resultAmount = Physics2D.CapsuleCastNonAlloc(owner.transform.position, attackData.AttackSize, CapsuleDirection2D.Vertical, Vector2.Angle(Vector2.up, direction), direction, targetsHit, 1, enemyLayer);
        var resultAmount = Physics2D.OverlapBoxNonAlloc((Vector2) owner.transform.position + direction,
            attackData.AttackSize, 0, targetsHit, enemyLayer);
        OnAttack(direction);
        for (int i = 0; i < resultAmount; i++)
        {
            var target = targetsHit[i].GetComponent<ITakeDamage>();
            HitTarget(target, attackData.Damage);
        }
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
            owner.PlayerScoreTracker.AddScore();
        }
    }
}