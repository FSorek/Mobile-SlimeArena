using UnityEngine;

public class GameBossFight : IState
{
    public bool IsBossKilled { get; private set; }
    public GameBossFight()
    {

    }
    public void StateEnter()
    {
        IsBossKilled = false;
    }

    public void ListenToState()
    {
        if (EnemyNPC.Alive.Count <= 0 && !IsBossKilled)
            IsBossKilled = true;
    }

    public void StateExit()
    {
    }
}