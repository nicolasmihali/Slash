using UnityEngine;

public class WeaponBlockState : WeaponBaseState
{
    public WeaponBlockState(WeaponStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime("SwordBlock", 0.1f);
    }

    public override void Tick(float deltaTime)
    {
        if (!stateMachine.InputReader.BlockInput)
        {
            stateMachine.SwitchState(new WeaponIdleState(stateMachine));
        }
    }

    public override void Exit()
    {

    }
}
