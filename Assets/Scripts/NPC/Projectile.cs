using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float lifetime = 2f;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask wallLayer; 
    private Vector2 shootDirection;
    private NPCAttackData attackData;
    private RaycastHit2D[] wallCheck = new RaycastHit2D[1];
    private float shotTime;


    public void Shoot(Transform target, NPCAttackData attackData)
    {
        this.attackData = attackData;
        shootDirection = (target.position - transform.position + Vector3.up + new Vector3(0,Random.Range(-attackData.AccuracyOffset, attackData.AccuracyOffset),0)).normalized;
        shotTime = Time.time;
    }

    private void Update()
    {
        var hits = Physics2D.RaycastNonAlloc(transform.position, shootDirection, wallCheck, wallCheckDistance, wallLayer);
        if(hits > 0 || Time.time - shotTime >= lifetime)
            ProjectilePool.Instance.ReturnToPool(this);
        transform.Translate(Time.deltaTime * moveSpeed * shootDirection);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<Player>();
        if (player != null)
        {
            player.TakeDamage(attackData.Damage);
        }
        ProjectilePool.Instance.ReturnToPool(this);
    }
}