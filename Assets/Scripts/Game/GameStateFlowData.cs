using UnityEngine;
using UnityEngine.Playables;

[CreateAssetMenu(fileName = "Game Flow Data")]
public class GameStateFlowData : ScriptableObject
{
    [SerializeField] private RegularStageSpawningData[] spawningData;
    [SerializeField] private EnemyNPC bossPrefab;
    [SerializeField] private Vector2 bossSpawnPosition;
    private GameRegularStage gameStage;
    private GameBossCinematic bossCinematic;
    private GameBossFight bossStage;
    private PlayableDirector playableDirector;

    public GameRegularStage GameStage
    {
        get
        {
            if(gameStage == null)
                gameStage = new GameRegularStage(spawningData, NpcSpawnerSystem.Instance);
            return gameStage;
        }
    }

    public GameBossCinematic BossCinematic
    {
        get
        {
            if(bossCinematic == null)
                bossCinematic = new GameBossCinematic(playableDirector, bossPrefab, bossSpawnPosition, NpcSpawnerSystem.Instance);
            return bossCinematic;
        }
    }

    public GameBossFight BossStage
    {
        get
        {
            if(bossStage == null)
                bossStage = new GameBossFight();
            return bossStage;
        }
    }

    private void Awake()
    {
        playableDirector = FindObjectOfType<PlayableDirector>();
    }
}