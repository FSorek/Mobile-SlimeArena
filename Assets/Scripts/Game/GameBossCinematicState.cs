using UnityEngine;
using UnityEngine.Playables;

public class GameBossCinematicState : IState
{
    private readonly PlayableDirector director;
    private readonly Vector2 bossSpawnPosition;
    private Spawner spawner;
    public bool IsCinematicFinished { get; private set; }

    public GameBossCinematicState(PlayableDirector director, ObjectPool bossPool, Vector2 bossSpawnPosition)
    {
        this.director = director;
        this.bossSpawnPosition = bossSpawnPosition;
        spawner = new Spawner(bossPool);
    }
    
    public void StateEnter()
    {
        foreach (var enemy in EnemyNPC.Alive)
        {
            enemy.Health.TakeDamage(enemy.Health.CurrentHealth);
        }
        director.Play();
        spawner.SpawnAt(bossSpawnPosition);
    }

    public void ListenToState()
    {
        if (director.state != PlayState.Playing && !IsCinematicFinished)
            IsCinematicFinished = true;
    }

    public void StateExit()
    {
        
    }
}