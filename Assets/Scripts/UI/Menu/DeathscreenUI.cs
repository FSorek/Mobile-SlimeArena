using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathscreenUI : MonoBehaviour
{
    [SerializeField] private GameObject deathscreen;
    private Player player;
    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }
    private void Start()
    {
        player.Health.OnDeath += () => DeathscreenSetActive(true);
        player.OnSpawn += () => DeathscreenSetActive(false);
        DeathscreenSetActive(false);
    }

    private void DeathscreenSetActive(bool active)
    {
        if(deathscreen.activeSelf != active)
            deathscreen.SetActive(active);
    }
}
