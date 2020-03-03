using System.Collections.Generic;
using UnityEngine;

public class GameRegularStage : IState
{
    private readonly List<(float, Spawner)> timerSpawners;
    private float lastSpawnTime;
    public GameRegularStage(List<(float, Spawner)> timerSpawners)
    {
        this.timerSpawners = timerSpawners;
    }
    public void StateEnter()
    {
    }

    public void ListenToState()
    {
        foreach (var timerSpawner in timerSpawners)
        {
            if(timerSpawner.Item2.IsSpawning 
               || Time.time - timerSpawner.Item2.LastSpawnTime < timerSpawner.Item1)
                return;

            timerSpawner.Item2.OrderSpawn();
            lastSpawnTime = Time.time;
        }
    }

    public void StateExit()
    {
    }
}