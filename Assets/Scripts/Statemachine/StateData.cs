using System;

public abstract class StateData<T> where T : Enum
{
    private T currentState;
    public event Action<T> OnStateChanged = delegate {  };
    public T CurrentState => currentState;

    public void ChangeState(T state)
    {
        currentState = state;
        OnStateChanged(currentState);
    }
}