using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNPC : MonoBehaviour, ITakeDamage, IGameObjectPooled
{
    public static List<EnemyNPC> Alive;
    [SerializeField] private int maxHealth;
    [SerializeField] private float timeUntilBodyIsGone = 2f;
    [SerializeField] private Transform attackOrigin;

    private Collider2D activeCollider;

    public Transform Target { get; private set; }
    public Transform AttackOrigin => attackOrigin;
    public Health Health { get; private set; }
    public ObjectPool Pool { get; set; }

    private void Awake()
    {
        Health = new Health(maxHealth);
        activeCollider = GetComponent<Collider2D>();
        Target = FindObjectOfType<Player>().transform;
        Health.OnDeath += OnDeath;
    }

    private void OnDeath()
    {
        activeCollider.enabled = false;
        Invoke(nameof(ReturnToPool), timeUntilBodyIsGone);
    }

    private void OnEnable()
    {
        Alive.Add(this);
        Health?.Reset();
        activeCollider.enabled = true;
    }

    public void TakeDamage(int damage)
    {
        Health.TakeDamage(damage);

    }

    private void ReturnToPool()
    {
        Alive.Remove(this);
        gameObject.ReturnToPool();
    }
}