using UnityEngine;

public class NPCAttackState : IState
{
    private readonly NPCAttackData attackData;
    private readonly EnemyNPC owner;

    
    public NPCAttackState(NPCAttackData attackData, EnemyNPC owner)
    {
        this.owner = owner;
        this.attackData = attackData;
    }
    public void StateEnter()
    {
        var projectile = ProjectilePool.Instance.Get();
        projectile.transform.position = owner.transform.position;
        projectile.Shoot(owner.Target, attackData);
        projectile.gameObject.SetActive(true);
    }

    public void ListenToState()
    {

    }


    public void StateExit()
    {
        
    }

    public bool CanExit => true;
}