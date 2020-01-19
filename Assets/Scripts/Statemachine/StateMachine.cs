using System.Collections.Generic;

public class StateMachine<T>
{
    private Dictionary<T, IState> availablePlayerStates = new Dictionary<T, IState>();
    private StateData<T> data;
    private IState currentState;

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
    }


    public void RegisterState(T key, IState state)
    {
        availablePlayerStates.Add(key, state);
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
        bool condition = (availablePlayerStates.ContainsKey(key) ||
                          currentState == availablePlayerStates[key]);
        return condition;
    }
}