using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T> where T : Enum
{
    private readonly Dictionary<T, IState> availableStates = new Dictionary<T, IState>();
    private readonly List<StateTransition<T>> transitions = new List<StateTransition<T>>();
    private readonly Dictionary<T, float> lastExitTimes = new Dictionary<T, float>();
    private readonly StateData<T> data;
    
    private IState currentState;
    
    public StateMachine(StateData<T> stateData)
    {
        data = stateData;
        data.OnStateChanged += DataOnStateChanged;
    }
    public void Tick()
    {
        if(currentState == null)
        {
            data.ChangeState(data.CurrentState);
        }
        currentState.ListenToState();
        if(!currentState.CanExit)
            return;

        StateTransition<T> transition = CheckTransition();
        if (transition != null && transition.CanTransition)
        {
            data.ChangeState(transition.ToState);
            transition.TransitionCallback?.Invoke();
        }
    }

    private StateTransition<T> CheckTransition()
    {
        foreach (StateTransition<T> transition in transitions)
        {
            if (Equals(transition.FromState, data.CurrentState))
            {
                return transition;
            }
        }
        return null;
    }

    public void RegisterState(T key, IState state)
    {
        availableStates.Add(key, state);
        lastExitTimes.Add(key, -Mathf.Infinity);
    }

    public void CreateTransition(T fromState, T toState, Func<bool> condition, Action onTransitionCallback = null)
    {
        var transition = new StateTransition<T>(fromState, toState, condition, onTransitionCallback);
        transitions.Add(transition);
    }

    public float TimeSinceStateExit(T state)
    {
        if (lastExitTimes.ContainsKey(state))
            return Time.fixedTime - lastExitTimes[state];
        return Mathf.Infinity;
    }

    private void DataOnStateChanged(T key)
    {
        if (availableStates.ContainsKey(key))
        {
            currentState?.StateExit();
            lastExitTimes[key] = Time.fixedTime;
            currentState = availableStates[key];
            currentState.StateEnter();
        }
    }
}