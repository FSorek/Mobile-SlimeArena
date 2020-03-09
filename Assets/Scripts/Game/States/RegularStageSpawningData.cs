using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RegularStageSpawningData
{
    [SerializeField] private float spawnFrequency;
    [SerializeField] private EnemyNPC spawnedNpc;
    private IEntityStateMachine npcStateMachineType;

    public float SpawnFrequency => spawnFrequency;

    public IEntityStateMachine NpcStateMachineType
    {
        get
        {
            if (npcStateMachineType == null)
                npcStateMachineType = spawnedNpc.GetComponent<IEntityStateMachine>();
            return npcStateMachineType;
        }
    }
}

