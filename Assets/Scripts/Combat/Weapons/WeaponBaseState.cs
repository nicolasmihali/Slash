using UnityEngine;

public abstract class WeaponBaseState : State
{
    protected WeaponStateMachine stateMachine;

    public WeaponBaseState(WeaponStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
}
