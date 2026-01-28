using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    protected State currentState;

    public string CurrentStateName =>
        currentState != null ? currentState.GetType().Name : "None";

    private void Update()
    {
        currentState?.Tick(Time.deltaTime);
    }

    public void SwitchState(State newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }
}
