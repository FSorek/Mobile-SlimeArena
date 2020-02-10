using System;
using System.Collections.Generic;

public class StateMachine<T> where T : System.Enum
{
    private Dictionary<T, IState> availablePlayerStates = new Dictionary<T, IState>();
    private StateData<T> data;
    private IState currentState;
    private Dictionary<T, List<StateTransition<T>>> transitions = new Dictionary<T, List<StateTransition<T>>>();
    
    public StateMachine(StateData<T> stateData)
    {
        data = stateData;
        data.OnStateEntered += DataOnStateEntered;
        data.OnStateExit += DataOnStateExit;
    }
    public void Tick()
    {
        if(currentState == null)
        {
            currentState = availablePlayerStates[data.CurrentState];
            currentState.StateEnter();
        }
        currentState.ListenToState();
        if(!transitions.ContainsKey(data.CurrentState) || !currentState.CanExit)
            return;

        foreach (StateTransition<T> transition in transitions[data.CurrentState])
        {
            if (transition.CanTransition)
            {
                data.ChangeState(transition.ToState);
            }
        }
    }


    public void RegisterState(T key, IState state)
    {
        availablePlayerStates.Add(key, state);
    }

    public void CreateTransition(T fromState, T toState, Func<bool> condition)
    {
        if(!transitions.ContainsKey(fromState))
                transitions[fromState] = new List<StateTransition<T>>();
        var transition = new StateTransition<T>(toState, condition);
        transitions[fromState].Add(transition);
    }

    public IState GetState(T key)
    {
        return availablePlayerStates.ContainsKey(key) ? currentState : null;
    }

    private void DataOnStateEntered(T key)
    {
        if (!ValidateState(key)) return;
        currentState = availablePlayerStates[key];
        currentState.StateEnter();
    }

    private void DataOnStateExit(T key)
    {
        if (currentState != null && ValidateState(key))
            currentState.StateExit();
    }
        
    private bool ValidateState(T key)
    {
        if (availablePlayerStates.ContainsKey(key))
            return currentState != availablePlayerStates[key];
        return false;
    }
}

public class StateTransition<T> where T : System.Enum
{
    private readonly T toState;
    private readonly Func<bool> condition;

    public bool CanTransition => condition.Invoke();
    public T ToState => toState;

    public StateTransition(T toState, Func<bool> condition)
    {
        this.toState = toState;
        this.condition = condition;
    }
}