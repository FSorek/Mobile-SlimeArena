using System;
using System.Collections.Generic;
using UnityEngine;

public class GameRegularStage : IState
{
    private readonly RegularStageSpawningData[] spawningData;
    private NpcSpawnerSystem npcSpawnerSystem;

    public GameRegularStage(RegularStageSpawningData[] spawningData, NpcSpawnerSystem spawnerSystem)
    {
        this.spawningData = spawningData;
        npcSpawnerSystem = spawnerSystem;
    }
    public void StateEnter()
    {
        
    }

    public void ListenToState()
    {
        foreach (var spawnerData in spawningData)
        {
            var npcType = spawnerData.NpcStateMachineType;
            var lastSpawnTime = npcSpawnerSystem.GetLastSpawnTime(npcType);
            if(Time.time - lastSpawnTime >= spawnerData.SpawnFrequency)
                npcSpawnerSystem.RequestSpawnOutsideView(npcType);
        }
    }

    public void StateExit()
    {
    }
}