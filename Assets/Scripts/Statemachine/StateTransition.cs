using System;

public class StateTransition<T> where T : Enum
{
    private readonly T fromState;
    private readonly T toState;
    private readonly Func<bool> condition;
    private readonly Action callback;

    public T FromState => fromState;
    public T ToState => toState;
    public bool CanTransition => condition.Invoke();

    public Action TransitionCallback => callback;

    public StateTransition(T fromState, T toState, Func<bool> condition, Action callback)
    {
        this.fromState = fromState;
        this.toState = toState;
        this.condition = condition;
        this.callback = callback;
    }
}