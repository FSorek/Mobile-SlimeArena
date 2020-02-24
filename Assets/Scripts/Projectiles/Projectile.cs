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
    private int damage;
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

    public void Shoot(Vector2 targetPosition, NPCAttackData attackData)
    {
        damage = attackData.Damage;
        var directionToTarget = (targetPosition - (Vector2) transform.position).normalized;
        shootDirection = (directionToTarget + Random.insideUnitCircle * attackData.AccuracySpread).normalized;
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
            damagable.Health.TakeDamage(damage);
        }
        gameObject.ReturnToPool();
    }

    public ObjectPool Pool { get; set; }
}