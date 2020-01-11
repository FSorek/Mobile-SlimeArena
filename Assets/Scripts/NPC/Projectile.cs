using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Vector2 shootDirection;
    private NPCAttackData attackData;


    public void Shoot(Transform target, NPCAttackData attackData)
    {
        this.attackData = attackData;
        shootDirection = (target.position - transform.position + Vector3.up + new Vector3(0,Random.Range(-attackData.AccuracyOffset, attackData.AccuracyOffset),0)).normalized;
    }

    private void Update()
    {
        transform.Translate(Time.deltaTime * moveSpeed * shootDirection);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var playerHP = other.GetComponent<PlayerHealth>();
        if (playerHP != null)
        {
            playerHP.TakeDamage(attackData.Damage);
        }
        ProjectilePool.Instance.ReturnToPool(this);
    }
}