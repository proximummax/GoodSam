using System;

public class AIStateMachine
{
    private AIState[] _states;
    private AIAgent _agent;
    private AIStateID _currentState;

    public AIStateMachine(AIAgent agent)
    {
        _agent = agent;
        int numStates = Enum.GetNames(typeof(AIStateID)).Length;
        _states = new AIState[numStates];
    }

    public void RegisterState(AIState state)
    {
        int index = (int)state.GetID();
        _states[index] = state;
    }

    private AIState GetState(AIStateID stateID)
    {
        int index = (int)stateID;
        return _states[index];
    }

    public void Update()
    {
        GetState(_currentState)?.Update(_agent);
    }

    public void ChangeState(AIStateID newState)
    {
        GetState(_currentState)?.Exit(_agent);
        _currentState = newState;
        GetState(_currentState)?.Enter(_agent);
    }
}