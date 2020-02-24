﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNPC : MonoBehaviour, ITakeDamage, IGameObjectPooled
{
    public static readonly List<EnemyNPC> Alive = new List<EnemyNPC>();
    [SerializeField] private int maxHealth;
    [SerializeField] private float timeUntilBodyIsGone = 2f;
    [SerializeField] private Transform attackOrigin;

    private Collider2D activeCollider;
    private LayerMask obstacleMask;

    public Transform Target { get; private set; }
    public Transform AttackOrigin => attackOrigin;
    public Health Health { get; private set; }
    public ObjectPool Pool { get; set; }

    private void Awake()
    {
        Health = new Health(maxHealth);
        activeCollider = GetComponent<Collider2D>();
        Target = FindObjectOfType<Player>().transform;
        Health.OnDeath += Death;
        obstacleMask = LayerMask.GetMask("World");
    }

    private void Death()
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

    private void ReturnToPool()
    {
        Alive.Remove(this);
        gameObject.ReturnToPool();
    }
    
    public bool HasLineOfSightTo(Vector2 targetPosition)
    {
        Vector2 attackPosition = attackOrigin.position;
        Vector2 directionToPlayer = (targetPosition - attackPosition).normalized;
        var distance = Vector2.Distance(attackPosition, targetPosition);

        RaycastHit2D hit = Physics2D.Raycast(attackPosition, directionToPlayer, distance, obstacleMask);
        return hit.collider == null;
    }
}