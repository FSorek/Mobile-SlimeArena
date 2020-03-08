using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    private Player player;
    private bool canSpawnHealthUp;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        player.Health.OnTakeDamage += PlayerOnTakeDamage;
    }

    private void PlayerOnTakeDamage(int damage)
    {
        canSpawnHealthUp = true;
    }
}
