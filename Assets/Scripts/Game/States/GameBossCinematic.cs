using UnityEngine;
using UnityEngine.Playables;

public class GameBossCinematic : IState
{
    private readonly PlayableDirector director;
    private readonly EnemyNPC npcPrefab;
    private readonly Vector2 bossSpawnPosition;
    private readonly NpcSpawnerSystem spawnerSystem;
    public bool IsCinematicFinished { get; private set; }

    public GameBossCinematic(PlayableDirector director, EnemyNPC npcPrefab, Vector2 bossSpawnPosition, NpcSpawnerSystem spawnerSystem)
    {
        this.director = director;
        this.npcPrefab = npcPrefab;
        this.bossSpawnPosition = bossSpawnPosition;
        this.spawnerSystem = spawnerSystem;
    }
    
    public void StateEnter()
    {
        for (int i = 0; i < EnemyNPC.Alive.Count; i++)
        {
            var enemy = EnemyNPC.Alive[i];
            enemy.Health.TakeDamage(enemy.Health.CurrentHealth);
        }

        director.Play();
        spawnerSystem.SpawnAt(bossSpawnPosition, npcPrefab.GetComponent<IEntityStateMachine>());
    }

    public void ListenToState()
    {
        if (director.state != PlayState.Playing && !IsCinematicFinished)
        {
            IsCinematicFinished = true;
        }
    }

    public void StateExit()
    {
        
    }
}