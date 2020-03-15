using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathscreenUI : MonoBehaviour
{
    [SerializeField] private GameObject deathscreen;
    private Player player;
    private ITakeDamage playerHealth;
    private void Awake()
    {
        player = FindObjectOfType<Player>();
        playerHealth = player.GetComponent<ITakeDamage>();
    }
    private void Start()
    {
        player.GetComponent<ITakeDamage>().OnTakeDamage += PlayerOnTakeDamage;
        player.OnSpawn += () => DeathscreenSetActive(false);
        DeathscreenSetActive(false);
    }

    private void PlayerOnTakeDamage(int amount)
    {
        if(playerHealth.CurrentHealth <= 0)
            DeathscreenSetActive(true);
    }

    private void DeathscreenSetActive(bool active)
    {
        if(deathscreen.activeSelf != active)
            deathscreen.SetActive(active);
    }
}
