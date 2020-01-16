using System;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public event Action<Vector2> OnAttack = delegate { };
    
    [SerializeField] private int damage;
    [SerializeField] private Vector2 attackSize;
    [SerializeField] private float attackDelay = .5f;
    private RaycastHit2D[] targetsHit = new RaycastHit2D[20];
    private float lastAttackTime;

    public void Attack(Vector2 direction)
    {
        if(Time.time - lastAttackTime < attackDelay)
            return;
        var boxAngle = Vector2.Angle(Vector2.up, direction);
        var resultAmount = Physics2D.BoxCastNonAlloc(transform.position, attackSize, boxAngle, direction, targetsHit);
        for (int i = 0; i < resultAmount; i++)
        {
            if(targetsHit[i].transform == transform)
                continue;
            
            Debug.Log(targetsHit[i].transform.name);
            var target = targetsHit[i].transform.GetComponent<ITakeDamage>();
            target?.TakeDamage(damage);
        }
        OnAttack(direction);
        lastAttackTime = Time.time;
    }
}