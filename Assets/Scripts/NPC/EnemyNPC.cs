using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNPC : MonoBehaviour, ICanAttack, IGameObjectPooled
{
    public static event Action<EnemyNPC> OnDespawned = delegate {  };
    public static event Action<EnemyNPC> OnDeath = delegate {  }; 
    [SerializeField] private float timeUntilBodyIsGone = 2f;
    [SerializeField] private Transform attackOrigin;

    private Collider2D activeCollider;
    private LayerMask obstacleMask;
    private Player target;
    private ITakeDamage healthComponent;

    public Transform AttackOrigin => attackOrigin;
    public Vector2 AttackDirection => (target.transform.position - transform.position).normalized;
    public ObjectPool Pool { get; set; }

    private void Awake()
    {
        activeCollider = GetComponent<Collider2D>();
        obstacleMask = LayerMask.GetMask("World");
        target = FindObjectOfType<Player>();
        healthComponent = GetComponent<ITakeDamage>();
        healthComponent.OnTakeDamage += Death;
    }

    private void Death(int amount)
    {
        if(healthComponent.CurrentHealth > 0)
            return;
        activeCollider.enabled = false;
        OnDeath(this);
        Invoke(nameof(ReturnToPool), timeUntilBodyIsGone);
    }

    private void OnDisable()
    {
        OnDespawned(this);
    }

    private void OnEnable()
    {
        activeCollider.enabled = true;
    }

    public void ReturnToPool()
    {
        if(Pool != null)
            gameObject.ReturnToPool();
        else
        {
            Debug.LogWarning("No pool assigned to the object, destroying");
            Destroy(gameObject);
        }
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