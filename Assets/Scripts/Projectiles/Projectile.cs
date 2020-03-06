using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Projectile : MonoBehaviour, IReflectable, IGameObjectPooled
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float lifetime = 2f;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask wallLayer; 
    private Vector2 shootDirection;
    private RaycastHit2D[] wallCheck = new RaycastHit2D[1];
    private float shotTime;
    private bool bouncedBack;
    private int playerLayer;
    private int enemyLayer;
    private Action<ITakeDamage> onHitCallback;

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

    public void Shoot(Vector2 direction, Action<ITakeDamage> onHitCallback)
    {
        shootDirection = direction;
        this.onHitCallback = onHitCallback;
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
        var target = other.GetComponent<ITakeDamage>();
        if (target != null)
            onHitCallback(target);
        gameObject.ReturnToPool();
    }

    public ObjectPool Pool { get; set; }
    public void Reflect()
    {
        shootDirection *= -1;
        gameObject.layer = playerLayer;
    }
}