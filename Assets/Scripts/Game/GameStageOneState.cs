using UnityEngine;

public class GameStageOneState : IState
{
    private readonly ObjectPool starterEnemy;
    private readonly float spawnFrequency;
    private float lastSpawnTime;
    private Spawner spawner;
    public GameStageOneState(ObjectPool starterEnemy, float spawnFrequency)
    {
        this.starterEnemy = starterEnemy;
        this.spawnFrequency = spawnFrequency;
        spawner = new Spawner(starterEnemy);
    }
    public void StateEnter()
    {
        
    }

    public void ListenToState()
    {
        if(spawner.IsSpawning 
           || Time.time - lastSpawnTime < spawnFrequency)
            return;

        spawner.OrderSpawn();
        lastSpawnTime = Time.time;
    }

    public void StateExit()
    {
        
    }
}