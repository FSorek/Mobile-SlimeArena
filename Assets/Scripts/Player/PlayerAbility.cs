using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private float damageRadius;
    [SerializeField] private int damagePerTick;
    private CameraFollow playerCamera;
    private Player player;
    private IMovement accelerometerMovement;
    private BoxCollider2D[] abilityWalls = new BoxCollider2D[4];
    private Collider2D[] targetColliders = new Collider2D[20];
    
    private void Awake()
    {
        playerCamera = Camera.main.GetComponent<CameraFollow>();
        accelerometerMovement = new AccelerometerMovement(transform);
        player = GetComponent<Player>();
        InitializeAbilityWalls();
    }

    public void UseAbility()
    {
        playerCamera.AllowFollow(false);
        player.ChangeMovementStyle(accelerometerMovement);
        InvokeRepeating(nameof(DealAreaDamage), .5f, .5f);
    }

    public void StopAbility()
    {
        playerCamera.AllowFollow(true);
        player.ResetMovementStyle();
    }

    private void DealAreaDamage()
    {
        int targets = Physics2D.OverlapCircleNonAlloc(transform.position, damageRadius, targetColliders);
        for (int i = 0; i < targets; i++)
        {
            var availableTarget = targetColliders[i].GetComponent<ITakeDamage>();
            if(availableTarget == null)
                continue;
            
            availableTarget.TakeDamage(damagePerTick);
        }
    }

    private void InitializeAbilityWalls()
    {
        var verticalOffset = transform.position.y + playerCamera.GetComponent<Camera>().orthographicSize;
        var horizontalOffset = transform.position.x + playerCamera.GetComponent<Camera>().orthographicSize * Screen.width / Screen.height;
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
