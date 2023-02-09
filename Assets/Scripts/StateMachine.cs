using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    protected State CurrentState;
    public void SetState(State state)
    {
        if (CurrentState != null)
        {
            CurrentState.OnStateExit();
        }
        CurrentState = state;
        CurrentState.OnStateEnter();

        //CurrentState.Idle();
        Debug.Log("State Machine");
    }
}
