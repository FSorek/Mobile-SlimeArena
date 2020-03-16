using System;
using System.Collections.Generic;
using UnityEngine;

public class NpcSpawnerSystem : MonoBehaviour
{
    public static NpcSpawnerSystem Instance { get; private set; }
    public event Action<EnemyNPC> OnSpawned = delegate {  };

    [SerializeField] private List<ObjectPool> spawnerPools;

    private Dictionary<Type, Spawner> spawners = new Dictionary<Type, Spawner>();
    private List<EnemyNPC> enemiesAlive = new List<EnemyNPC>();

    private void OnEnable()
    {
        spawners.Clear();
        foreach (var objectPool in spawnerPools)
        {
            var entityStateMachine = objectPool.ReadAssignedPrefab().GetComponent<IEntityStateMachine>();
            if(entityStateMachine == null)
                return;
        
            var spawner = new Spawner(objectPool);
            spawners.Add(entityStateMachine.GetType(), spawner);
        }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else if (Instance == null)
            Instance = this;
    }

    public float GetLastSpawnTime(IEntityStateMachine npc)
    {
        var npcType = npc.GetType();
        if(spawners.ContainsKey(npcType))
            return spawners[npcType].LastSpawnTime;
        return Mathf.Infinity;
    }

    public void SpawnAt(Vector2 position, IEntityStateMachine npc)
    {
        var npcType = npc.GetType();
        if (spawners.ContainsKey(npcType))
        {
            var spawnedNpc = spawners[npcType].SpawnAt(position).GetComponent<EnemyNPC>();
            Spawn(spawnedNpc);
        }
    }

    // Request means it might not happen on the same frame as the call to the method
    // it runs a coroutine to find a free(outside a collider) spawn position each frame until success
    public void RequestSpawnOutsideView(IEntityStateMachine npc)
    {
        var npcType = npc.GetType();
        if(spawners.ContainsKey(npcType))
        {
            spawners[npcType].OrderSpawn(Spawn);
        }
    }

    private void Spawn(EnemyNPC npc)
    {
        OnSpawned(npc);
        enemiesAlive.Add(npc);
    }

    public EnemyNPC[] GetEnemiesAlive()
    {
        return enemiesAlive.ToArray();
    }

    private void Update()
    {
        for (int i = 0; i < enemiesAlive.Count; i++)
        {
            var npc = enemiesAlive[i];
            if (!npc.isActiveAndEnabled)
            {
                enemiesAlive.Remove(npc);
            }
        }
    }
}