using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float spawnRate = 2f;
    [SerializeField] private GameObject spawnedPrefab;
    private Camera playerCamera;
    private float lastSpawnTime;

    private void Awake()
    {
        playerCamera = Camera.main;
    }

    private void Update()
    {
        if (Time.time - lastSpawnTime > spawnRate)
            Spawn();
    }

    private void Spawn()
    {
        Instantiate(spawnedPrefab, GetSpawnpoint(), Quaternion.identity);
        lastSpawnTime = Time.time;
    }

    private Vector2 GetSpawnpoint()
    {
        
        var point = playerCamera.ViewportToWorldPoint(new Vector2(1.1f, 1.1f));
        
        return point;
    }
}