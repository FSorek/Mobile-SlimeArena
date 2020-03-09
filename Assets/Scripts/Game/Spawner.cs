using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner
{
    private readonly ObjectPool prefabPool;
    private readonly Camera playerCamera;
    private readonly LayerMask unstuckMask;
    public bool IsSpawning { get; private set; }
    public float LastSpawnTime { get; private set; }

    public Spawner(ObjectPool prefabPool)
    {
        this.prefabPool = prefabPool;
        playerCamera = Camera.main;
        unstuckMask = LayerMask.GetMask("World");
    }

    public void OrderSpawn(Action<EnemyNPC> onSuccessCallback = null)
    {
        if(IsSpawning)
            return;
        
        IsSpawning = true;
        var prefabInstance = prefabPool.Get();
        prefabPool.StartCoroutine(TrySpawn(prefabInstance, onSuccessCallback));
    }

    public GameObject SpawnAt(Vector2 position)
    {
        var prefabInstance = prefabPool.Get();
        prefabInstance.transform.position = position;
        prefabInstance.gameObject.SetActive(true);
        return prefabInstance;
    }

    private IEnumerator TrySpawn(GameObject prefabInstance, Action<EnemyNPC> onSuccessCallback)
    {
        var point = RandomPointOutsideCamera();
        var worldPosition = playerCamera.ViewportToWorldPoint(point);
        while (Physics2D.OverlapCircle(worldPosition, 1f, unstuckMask) != null)
        {
            point = RandomPointOutsideCamera();
            worldPosition = playerCamera.ViewportToWorldPoint(point);
            yield return null;
        }

        prefabInstance.transform.position = new Vector3(worldPosition.x, worldPosition.y, 0);
        prefabInstance.gameObject.SetActive(true);
        onSuccessCallback?.Invoke(prefabInstance.GetComponent<EnemyNPC>());
        LastSpawnTime = Time.time;
        IsSpawning = false;
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
        
        return new Vector2(x,y);
    }
}