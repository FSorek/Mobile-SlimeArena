using System;
using UnityEngine;

public class EnemyNPC : MonoBehaviour, ITakeDamage, IGameObjectPooled
{
    [SerializeField] private int maxHealth;
    [SerializeField] private float timeUntilBodyIsGone = 2f;
    [SerializeField] private Transform attackOrigin;

    private Collider2D activeCollider;
    private Transform target;

    public Transform Target => target;
    public Transform AttackOrigin => attackOrigin;
    public Health Health { get; private set; }
    public ObjectPool Pool { get; set; }

    private void Awake()
    {
        Health = new Health(maxHealth);
        activeCollider = GetComponent<Collider2D>();
        target = FindObjectOfType<Player>().transform;
        Health.OnDeath += OnDeath;
    }

    private void OnDeath()
    {
        activeCollider.enabled = false;
        Invoke(nameof(ReturnToPool), timeUntilBodyIsGone);
    }

    private void OnEnable()
    {
        Health.Reset();
        activeCollider.enabled = true;
    }

    public void TakeDamage(int damage)
    {
        Health.TakeDamage(damage);

    }

    private void ReturnToPool()
    {
        gameObject.ReturnToPool();
    }
}