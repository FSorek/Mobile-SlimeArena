using System;
using UnityEngine;
using UnityEngine.Playables;

[CreateAssetMenu(fileName = "Game Flow Data")]
public class GameSceneStateFlowData : ScriptableObject
{
    [SerializeField] private RegularStageSpawningData[] spawningData;
    [SerializeField] private EnemyNPC bossPrefab;
    [SerializeField] private Vector2 bossSpawnPosition;

    private GameRegularStage gameStage;
    private GameBossCinematic bossCinematic;
    private GameBossFight bossFight;

    public GameRegularStage GetGameStage()
    {
        if(gameStage == null)
            gameStage = new GameRegularStage(spawningData, NpcSpawnerSystem.Instance);
        return gameStage;
    }
    
    public GameBossCinematic GetBossCinematic()
    {
        if(bossCinematic == null)
            bossCinematic = new GameBossCinematic(FindObjectOfType<PlayableDirector>(), bossPrefab, bossSpawnPosition, NpcSpawnerSystem.Instance);
        return bossCinematic;
    }
    
    public GameBossFight GetBossStage()
    {
        if(bossFight == null)
            bossFight = new GameBossFight(NpcSpawnerSystem.Instance);
        return bossFight;
    }
}