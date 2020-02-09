using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    [SerializeField] private Spawner spawner;
    [SerializeField] private int scorePerNewSpawner;
    [SerializeField] private int maxSpawners;

    private void Awake()
    {
        Instantiate(spawner);
    }

    private void Start()
    {
        var player = FindObjectOfType<Player>();
        player.PlayerScoreTracker.OnScoreIncreased += PlayerOnScoreIncreased;
    }

    private void PlayerOnScoreIncreased(int score)
    {
        if(score % scorePerNewSpawner != 0 || score > scorePerNewSpawner * maxSpawners)
            return;
        Instantiate(spawner);
    }
}
