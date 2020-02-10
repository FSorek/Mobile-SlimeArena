using System;

public abstract class StateData<T> where T : Enum
{
    private T currentState;
    public event Action<T> OnStateEntered = delegate {  };
    public event Action<T> OnStateExit = delegate {  };
    public T CurrentState => currentState;

    public void ChangeState(T state)
    {
        OnStateExit(CurrentState);
        currentState = state;
        OnStateEntered(CurrentState);
    }
}