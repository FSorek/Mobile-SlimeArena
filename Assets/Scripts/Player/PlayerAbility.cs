﻿using System.Collections;
using UnityEngine;
using Object = UnityEngine.Object;

public class PlayerAbility
{
    private readonly Player owner;
    private readonly PlayerAbilityData abilityData;
    private readonly CameraFollow playerCamera;
    private readonly IMovement accelerometerMovement;
    private readonly BoxCollider2D[] abilityWalls = new BoxCollider2D[4];
    private readonly Collider2D[] targetColliders = new Collider2D[20];
    private bool isUsingAbility;
    private float currentPool;
    private float lastDamageTime;

    public bool IsUsingAbility => isUsingAbility;
    public double CurrentPoolPercentage => currentPool / abilityData.MaxPoolAmount;

    public PlayerAbility(Player owner, PlayerAbilityData abilityData)
    {
        this.owner = owner;
        this.abilityData = abilityData;
        
        playerCamera = Camera.main.GetComponent<CameraFollow>();
        accelerometerMovement = new AccelerometerMovement(owner.GetComponent<Rigidbody2D>(), abilityData.TornadoMovespeed);
        owner.PlayerAttack.OnTargetHit += PlayerOnTargetHit;
        
        InitializeAbilityWalls();
    }

    public void Reset()
    {
        currentPool = abilityData.MaxPoolAmount;
    }

    private void PlayerOnTargetHit(ITakeDamage target)
    {
        if(target.IsDead && !isUsingAbility)
            RefillPool();
    }

    public void UseAbility()
    {
        if(currentPool < abilityData.TickRate)
            return;
        playerCamera.AllowFollow(false);
        owner.ChangeMovementStyle(accelerometerMovement);
        isUsingAbility = true;
    }

    public void Tick()
    {
        if (isUsingAbility && Time.time - lastDamageTime >= abilityData.TickRate)
        {
            DealAreaDamage();
        }
    }

    public void StopAbility()
    {
        playerCamera.AllowFollow(true);
        owner.ResetMovementStyle();
        isUsingAbility = false;
    }

    public void RefillPool()
    {
        if(currentPool < abilityData.MaxPoolAmount)
            currentPool += abilityData.TickRate;
        if (currentPool > abilityData.MaxPoolAmount)
            currentPool = abilityData.MaxPoolAmount;
    }

    private void DealAreaDamage()
    {
        int targets = Physics2D.OverlapCircleNonAlloc(owner.transform.position, abilityData.DamageRadius, targetColliders);
        for (int i = 0; i < targets; i++)
        {
            var availableTarget = targetColliders[i].GetComponent<ITakeDamage>();
            availableTarget?.TakeDamage(abilityData.DamagePerTick);
        }

        currentPool -= abilityData.TickRate;
        if (currentPool <= 0)
        {
            StopAbility();
            currentPool = 0;
        }
        lastDamageTime = Time.time;
    }

    private void InitializeAbilityWalls()
    {
        var verticalOffset = playerCamera.GetComponent<Camera>().orthographicSize;
        var horizontalOffset = playerCamera.GetComponent<Camera>().orthographicSize * playerCamera.GetComponent<Camera>().aspect;
        for (int i = 0; i < 4; i++)
        {
            abilityWalls[i] = (Object.Instantiate(abilityData.WallPrefab, playerCamera.transform)).GetComponent<BoxCollider2D>();
            if(i % 2 == 0)
                abilityWalls[i].size = new Vector2(1, 2 * verticalOffset);
            else
                abilityWalls[i].size = new Vector2(2 * horizontalOffset, 1);
        }

        abilityWalls[0].transform.localPosition = new Vector3(horizontalOffset,0,0);
        abilityWalls[1].transform.localPosition = new Vector3(0,verticalOffset,0);
        abilityWalls[2].transform.localPosition = new Vector3(-horizontalOffset,0,0);
        abilityWalls[3].transform.localPosition = new Vector3(0,-verticalOffset,0);
    }
}