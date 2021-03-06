using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private List<StateTransition> _stateTransitions = new List<StateTransition>();
    private List<StateTransition> _anyStateTransitions = new List<StateTransition>();

    private IState _currentState;
    private IState _previousState;
    
    public IState CurrentState => _currentState;
    public event Action<IState, IState> OnStateChanged;

    public void AddTransition(IState from, IState to, Func<bool> condition)
    {
        var stateTransition = new StateTransition(from, to, condition);
        _stateTransitions.Add(stateTransition);
    }

    public void AddAnyTransition(IState to, Func<bool> condition)
    {
        var stateTransition = new StateTransition(null, to, condition);
        _anyStateTransitions.Add(stateTransition);
    }

    public void SetState(IState state)
    {
        if (_currentState == state)
            return;

        _previousState = _currentState;
        _currentState?.OnExit();

        _currentState = state;
        Debug.Log($"Changed state to {state}");
        _currentState.OnEnter();
        
        OnStateChanged?.Invoke(_currentState, _previousState);
    }

    public void Tick()
    {
        StateTransition transition = CheckForTransition();
        if (transition != null)
        {
            SetState(transition.To);
        }

        _currentState.Tick();
    }

    private StateTransition CheckForTransition()
    {
        foreach (StateTransition transition in _anyStateTransitions)
        {
            if (transition.Condition())
            {
                return transition;
            }
        }
        
        foreach (StateTransition transition in _stateTransitions)
        {
            if (transition.From == _currentState && transition.Condition())
            {
                return transition;
            }
        }

        return null;
    }
}