using System;

public interface IEnemyStateMachine
{
    event Action<IState> OnEnemyStateChanged;
}