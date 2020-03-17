using System;
using UnityEngine;
using UnityEngine.Playables;
using Object = UnityEngine.Object;

//[CreateAssetMenu(fileName = "Game Flow Data")]
[Serializable]
public class GameSceneStateFlowData
{
    [SerializeField] private RegularStageSpawningData[] spawningData;
    [SerializeField] private EnemyNPC bossPrefab;
    [SerializeField] private Vector2 bossSpawnPosition;

    public RegularStageSpawningData[] SpawningData => spawningData;
    public EnemyNPC BossPrefab => bossPrefab;
    public Vector2 BossSpawnPosition => bossSpawnPosition;
}