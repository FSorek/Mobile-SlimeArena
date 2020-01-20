using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private float damageRadius;
    [SerializeField] private int damagePerTick;
    [SerializeField] private float tickRate = .5f;
    [SerializeField] private int maxPoolAmount = 3;
    private CameraFollow playerCamera;
    private Player player;
    private IMovement accelerometerMovement;
    private BoxCollider2D[] abilityWalls = new BoxCollider2D[4];
    private Collider2D[] targetColliders = new Collider2D[20];
    private bool isUsingAbility;
    private float currentPool;

    public bool IsUsingAbility => isUsingAbility;
    public double CurrentPoolPercentage => currentPool / maxPoolAmount;

    private void Awake()
    {
        playerCamera = Camera.main.GetComponent<CameraFollow>();
        accelerometerMovement = new AccelerometerMovement(transform);
        player = GetComponent<Player>();
        currentPool = maxPoolAmount;
        var playerAttack = GetComponent<PlayerAttack>();
        playerAttack.OnTargetHit += PlayerOnTargetHit;
        
        InitializeAbilityWalls();
    }

    private void PlayerOnTargetHit(ITakeDamage target)
    {
        if(target.IsDead && !isUsingAbility)
            RefillPool();
    }

    public void UseAbility()
    {
        if(currentPool < tickRate)
            return;
        playerCamera.AllowFollow(false);
        player.ChangeMovementStyle(accelerometerMovement);
        InvokeRepeating(nameof(DealAreaDamage), tickRate, tickRate);
        isUsingAbility = true;
    }

    public void StopAbility()
    {
        CancelInvoke(nameof(DealAreaDamage));
        playerCamera.AllowFollow(true);
        player.ResetMovementStyle();
        isUsingAbility = false;
    }

    public void RefillPool()
    {
        if(currentPool < maxPoolAmount)
            currentPool += tickRate;
        if (currentPool > maxPoolAmount)
            currentPool = maxPoolAmount;
    }

    private void DealAreaDamage()
    {
        int targets = Physics2D.OverlapCircleNonAlloc(transform.position, damageRadius, targetColliders);
        for (int i = 0; i < targets; i++)
        {
            var availableTarget = targetColliders[i].GetComponent<ITakeDamage>();
            availableTarget?.TakeDamage(damagePerTick);
        }

        currentPool -= tickRate;
        if (currentPool <= 0)
        {
            StopAbility();
            currentPool = 0;
        }
    }

    private void InitializeAbilityWalls()
    {
        var verticalOffset = playerCamera.GetComponent<Camera>().orthographicSize;
        var horizontalOffset = playerCamera.GetComponent<Camera>().orthographicSize * playerCamera.GetComponent<Camera>().aspect;
        for (int i = 0; i < 4; i++)
        {
            abilityWalls[i] = (Instantiate(wallPrefab, playerCamera.transform)).GetComponent<BoxCollider2D>();
            if(i % 2 == 0)
                abilityWalls[i].size = new Vector2(1, 2 * verticalOffset);
            else
                abilityWalls[i].size = new Vector2(2 * horizontalOffset, 1);
        }

        abilityWalls[0].transform.position = new Vector3(horizontalOffset,0,0);
        abilityWalls[1].transform.position = new Vector3(0,verticalOffset,0);
        abilityWalls[2].transform.position = new Vector3(-horizontalOffset,0,0);
        abilityWalls[3].transform.position = new Vector3(0,-verticalOffset,0);
    }
}
