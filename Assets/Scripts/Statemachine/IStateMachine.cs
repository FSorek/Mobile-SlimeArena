using System;

public interface IStateMachine
{
    event Action<IState> OnEnemyStateChanged;
}