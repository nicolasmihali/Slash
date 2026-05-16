using UnityEngine;

public class PlayerAerialAttackState : PlayerBaseState
{
    private float _verticalVelocity;
    //private const float Gravity = -4f;

    public PlayerAerialAttackState(PlayerStateMachine stateMachine, float launchForce) : base(stateMachine)
    {
        _verticalVelocity = launchForce;
    }

    public override void Enter()
    {
        
    }

    public override void Tick(float deltaTime)
    {
        _verticalVelocity += stateMachine.Gravity * deltaTime;

        if (_verticalVelocity < 0f)
            _verticalVelocity = Mathf.Max(_verticalVelocity, -2f);

        stateMachine.Controller.Move(new Vector3(0, _verticalVelocity, 0) * deltaTime);

        if (stateMachine.IsGrounded() && _verticalVelocity < 0f)
        {
            stateMachine.SwitchState(new PlayerIdleState(stateMachine));
        }
    }

    public override void Exit()
    {
        
    }

}
