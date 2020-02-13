﻿using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    
    [SerializeField] private float spawnRate = 2f;
    [SerializeField] private Vector2 spawnRateRandomOffset;
    [SerializeField] private ObjectPool prefabPool;

    private Camera playerCamera;
    private float lastSpawnTime;
    private LayerMask unstuckMask;
    private float randomizedSpawnRate;

    private void Awake()
    {
        playerCamera = Camera.main;
        unstuckMask = LayerMask.GetMask("World");
        randomizedSpawnRate = spawnRate + Random.Range(spawnRateRandomOffset.x, spawnRateRandomOffset.y);
        lastSpawnTime = Time.time;
    }

    private void Update()
    {
        if (Time.time - lastSpawnTime > randomizedSpawnRate)
            Spawn();
    }

    private void Spawn()
    {
        var enemy = prefabPool.Get();
        enemy.transform.position = RandomPointOutsideCamera();
        enemy.gameObject.SetActive(true);
        lastSpawnTime = Time.time;
        randomizedSpawnRate = spawnRate + Random.Range(spawnRateRandomOffset.x, spawnRateRandomOffset.y);
    }

    private Vector2 RandomPointOutsideCamera()
    {
        float x = 0, y = 0;
        float sideChance = Random.value;
        if (sideChance <= .25f)
        {
            x = 1.1f;
            y = Random.Range(0f, 1f);
        }
        else if (sideChance > .25f && sideChance <= .5f)
        {
            x = -0.1f;
            y = Random.Range(0f, 1f);
        }
        else if (sideChance > .5f && sideChance <= .75f)
        {
            x = Random.Range(0f, 1f);
            y = 1.1f;
        }
        else if (sideChance > .75f && sideChance <= 1)
        {
            x = Random.Range(0f, 1f);
            y = -0.1f;
        }

        var point = playerCamera.ViewportToWorldPoint(new Vector2(x, y));
        if (Physics2D.OverlapCircle(point, 1f, unstuckMask))
            return RandomPointOutsideCamera();

        return point;
    }
}