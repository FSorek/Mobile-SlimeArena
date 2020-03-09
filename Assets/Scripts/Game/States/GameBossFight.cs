using UnityEngine;

public class GameBossFight : IState
{
    private readonly NpcSpawnerSystem spawnerSystem;
    public bool IsBossKilled { get; private set; }
    public GameBossFight(NpcSpawnerSystem spawnerSystem)
    {
        this.spawnerSystem = spawnerSystem;
    }
    public void StateEnter()
    {
        IsBossKilled = false;
    }

    public void ListenToState()
    {
        if (spawnerSystem.GetEnemiesAlive().Length <= 0 && !IsBossKilled)
            IsBossKilled = true;
    }

    public void StateExit()
    {
    }
}