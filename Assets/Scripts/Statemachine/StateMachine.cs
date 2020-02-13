using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T> where T : System.Enum
{
    private Dictionary<T, IState> availablePlayerStates = new Dictionary<T, IState>();
    private Dictionary<T, float> lastExitTimes = new Dictionary<T, float>();
    
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
                transition.TransitionCallback?.Invoke();
                data.ChangeState(transition.ToState);
            }
        }
    }


    public void RegisterState(T key, IState state)
    {
        availablePlayerStates.Add(key, state);
        lastExitTimes.Add(key, -Mathf.Infinity);
    }

    public void CreateTransition(T fromState, T toState, Func<bool> condition, Action onTransitionCallback = null)
    {
        if(!transitions.ContainsKey(fromState))
                transitions[fromState] = new List<StateTransition<T>>();
        var transition = new StateTransition<T>(toState, condition, onTransitionCallback);
        transitions[fromState].Add(transition);
    }

    public IState GetState(T key)
    {
        return availablePlayerStates.ContainsKey(key) ? currentState : null;
    }

    public float TimeSinceStateExit(T state)
    {
        if (lastExitTimes.ContainsKey(state))
            return Time.fixedTime - lastExitTimes[state];
        return Mathf.Infinity;
    }

    private void DataOnStateEntered(T key)
    {
        if (availablePlayerStates.ContainsKey(key))
        {
            currentState = availablePlayerStates[key];
            currentState.StateEnter();
        }
    }

    private void DataOnStateExit(T key)
    {
        if (currentState != null && availablePlayerStates.ContainsKey(key))
        {
            currentState.StateExit();
            lastExitTimes[key] = Time.fixedTime;
        }
    }
}

public class StateTransition<T> where T : System.Enum
{
    private readonly T toState;
    private readonly Func<bool> condition;
    private readonly Action callback;

    public bool CanTransition => condition.Invoke();
    public T ToState => toState;

    public Action TransitionCallback => callback;

    public StateTransition(T toState, Func<bool> condition, Action callback)
    {
        this.toState = toState;
        this.condition = condition;
        this.callback = callback;
    }
}