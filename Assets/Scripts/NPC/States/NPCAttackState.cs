﻿using UnityEngine;

public class NPCAttackState : IState
{
    private readonly NPCAttackData attackData;
    private readonly EnemyNPC owner;
    private readonly NPCStateData stateData;

    private float lastAttackTime;
    
    public NPCAttackState(NPCAttackData attackData, EnemyNPC owner, NPCStateData stateData)
    {
        this.owner = owner;
        this.stateData = stateData;
        this.attackData = attackData;
        lastAttackTime = 0f;
        
        
    }
    public void StateEnter()
    {
        if(!CanAttack()) 
            return;
        
        var projectile = ProjectilePool.Instance.Get();
        projectile.transform.position = owner.transform.position;
        projectile.Shoot(owner.Target, attackData);
        projectile.gameObject.SetActive(true);
        lastAttackTime = Time.time;
    }

    public void ListenToState()
    {
        if(Vector2.Distance(owner.transform.position, owner.Target.position) > owner.AttackRange)
            stateData.ChangeState(NPCStates.GoWithinRange);
        else if(CanAttack())
            stateData.ChangeState(NPCStates.RepositionAttack);
    }

    private bool CanAttack() => Time.fixedTime - lastAttackTime >= attackData.ShootingRate;

    public void StateExit()
    {
        
    }
}