using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Projectile : MonoBehaviour, IGameObjectPooled
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float lifetime = 2f;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask wallLayer; 
    private Vector2 shootDirection;
    private NPCAttackData attackData;
    private RaycastHit2D[] wallCheck = new RaycastHit2D[1];
    private float shotTime;
    private bool bouncedBack;
    private int playerLayer;
    private int enemyLayer;

    private void Awake()
    {
        playerLayer = LayerMask.NameToLayer("Player");
        enemyLayer = LayerMask.NameToLayer("NPC");
    }

    private void OnEnable()
    {
        bouncedBack = false;
        gameObject.layer = enemyLayer;
    }

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
            gameObject.ReturnToPool();
        transform.Translate(Time.deltaTime * moveSpeed * shootDirection);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var damagable = other.GetComponent<ITakeDamage>();
        if (damagable != null)
        {
            damagable.Health.TakeDamage(attackData.Damage);
        }
        gameObject.ReturnToPool();
    }

    public void TakeDamage(int damage)
    {
        if(bouncedBack)
            return;
        gameObject.layer = playerLayer;
        shootDirection = -shootDirection;
        bouncedBack = true;
    }

    public ObjectPool Pool { get; set; }
}