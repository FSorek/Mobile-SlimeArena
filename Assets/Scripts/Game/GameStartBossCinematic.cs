using UnityEngine;
using UnityEngine.Playables;

public class GameStartBossCinematic : IState
{
    private readonly PlayableDirector director;
    private readonly Vector2 bossSpawnPosition;
    private Spawner spawner;
    public bool IsCinematicFinished { get; private set; }

    public GameStartBossCinematic(PlayableDirector director, ObjectPool bossPool, Vector2 bossSpawnPosition)
    {
        this.director = director;
        this.bossSpawnPosition = bossSpawnPosition;
        spawner = new Spawner(bossPool);
    }
    
    public void StateEnter()
    {
        for (int i = 0; i < EnemyNPC.Alive.Count; i++)
        {
            var enemy = EnemyNPC.Alive[i];
            enemy.Health.TakeDamage(enemy.Health.CurrentHealth);
        }

        director.Play();
        spawner.SpawnAt(bossSpawnPosition);
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