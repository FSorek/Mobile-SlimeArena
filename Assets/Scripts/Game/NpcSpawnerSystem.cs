using System;
using System.Collections.Generic;
using UnityEngine;

public class NpcSpawnerSystem : MonoBehaviour
{
    public static NpcSpawnerSystem Instance { get; private set; }
    
    public event Action<GameObject> OnSpawned = delegate {  };
    
    [SerializeField] private List<ObjectPool> spawnerPools;

    private Dictionary<Type, Spawner> spawners = new Dictionary<Type, Spawner>();

    private void Awake()
    {
        if (Instance != null)
            Destroy(this);
        else
            Instance = this;
        
        foreach (var objectPool in spawnerPools)
        {
            var entityStateMachine = objectPool.ReadAssignedPrefab().GetComponent<IEntityStateMachine>();
            if(entityStateMachine == null)
                return;
        
            var spawner = new Spawner(objectPool);
            spawners.Add(entityStateMachine.GetType(), spawner);
        }
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
            spawners[npcType].SpawnAt(position);
        }
    }

    // Request means it might not happen on the same frame as the call to the method
    // it runs a coroutine to find a free(outside a collider) spawn position each frame until success
    public void RequestSpawnOutsideView(IEntityStateMachine npc)
    {
        var npcType = npc.GetType();
        if(spawners.ContainsKey(npcType))
            spawners[npcType].OrderSpawn();
    }

}