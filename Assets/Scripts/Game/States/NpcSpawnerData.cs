using System;
using UnityEngine;

[Serializable]
public struct NpcSpawnerData
{
    [SerializeField] private float spawnFrequency;
    [SerializeField] private EnemyNPC prefab;
    public float SpawnFrequency => spawnFrequency;
    public EnemyNPC Npc => prefab;
}