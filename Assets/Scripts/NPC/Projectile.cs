using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float moveSpeed = 5f;
    private Vector2 shootDirection;
    public void SetTarget(Transform target)
    {
        shootDirection = (target.position - transform.position).normalized;
    }

    private void Update()
    {
        transform.Translate(Time.deltaTime * moveSpeed * shootDirection);
    }
}