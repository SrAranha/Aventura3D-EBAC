using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T> where T : System.Enum
{
    public Dictionary<T, StateBase> dictionaryState;
    public float timeToStart;
    public StateBase CurrentBase
    {
        get { return _currentState; }
    }

    private StateBase _currentState;

    public void Init()
    {
        dictionaryState = new Dictionary<T, StateBase>();
    }

    public void RegisterStates(T enumType, StateBase state)
    {
        dictionaryState.Add(enumType, state);
    }

    public void SwitchState(T state)
    {
        if (_currentState != null) _currentState.OnStateExit();
        
        _currentState = dictionaryState[state];

        _currentState.OnStateEnter();
    }

    public void Update()
    {
        if (_currentState != null) _currentState.OnStateStay();
    }
}
