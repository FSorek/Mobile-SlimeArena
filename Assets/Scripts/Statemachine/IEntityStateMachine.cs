using System;

public interface IEntityStateMachine
{
    event Action<IState> OnEntityStateChanged;
}