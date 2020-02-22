using System;
using UnityEngine;

public class EnemyNPC : MonoBehaviour, ITakeDamage, IGameObjectPooled
{
    public event Action OnDeath = delegate { };
    public event Action OnTakeDamage = delegate {  };
    [SerializeField] private int maxHealth;
    [SerializeField] private float timeUntilBodyIsGone = 2f;
    [SerializeField] private Transform attackOrigin;

    private Collider2D activeCollider;
    private Transform target;
    private Health health;

    public Transform Target => target;
    public bool IsDead => health.CurrentHealth <= 0;
    public ObjectPool Pool { get; set; }
    public Transform AttackOrigin => attackOrigin;

    private void Awake()
    {
        health = new Health(maxHealth);
        activeCollider = GetComponent<Collider2D>();
        target = FindObjectOfType<Player>().transform;
    }
    private void OnEnable()
    {
        health.Reset();
        activeCollider.enabled = true;
    }

    public void TakeDamage(int damage)
    {
        health.TakeDamage(damage);
        OnTakeDamage();
        if (IsDead)
        {
            activeCollider.enabled = false;
            Invoke(nameof(ReturnToPool), timeUntilBodyIsGone);
            OnDeath();
        }
    }

    private void ReturnToPool()
    {
        gameObject.ReturnToPool();
    }
}