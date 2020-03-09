using System;
using System.Collections.Generic;
using UnityEngine;

public class GameRegularStage : IState
{
    private readonly NpcSpawnerData[] npcSpawnerData;
    private NpcSpawnerSystem npcSpawnerSystem;

    public GameRegularStage(NpcSpawnerData[] npcSpawnerData, NpcSpawnerSystem spawnerSystem)
    {
        this.npcSpawnerData = npcSpawnerData;
        npcSpawnerSystem = spawnerSystem;
    }
    public void StateEnter()
    {
        
    }

    public void ListenToState()
    {
        foreach (var spawnerData in npcSpawnerData)
        {
            var npcType = spawnerData.Npc.GetComponent<IEntityStateMachine>();
            var lastSpawnTime = npcSpawnerSystem.GetLastSpawnTime(npcType);
            if(Time.time - lastSpawnTime >= spawnerData.SpawnFrequency)
                npcSpawnerSystem.RequestSpawnOutsideView(npcType);
        }
    }

    public void StateExit()
    {
    }
}