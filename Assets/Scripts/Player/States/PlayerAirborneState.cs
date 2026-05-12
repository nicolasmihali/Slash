using UnityEngine;

public class PlayerAirborneState : PlayerBaseState
{
    private Vector3 _playerVelocity;
    private float _speed;

    public PlayerAirborneState(PlayerStateMachine stateMachine, float speed, float yPlayerVelocity) : base(stateMachine)
    {
        _speed = speed;
        _playerVelocity = new Vector3(0, yPlayerVelocity, 0);
    }

    public override void Enter()
    {
        
    }

    public override void Tick(float deltaTime)
    {
        Vector3 movement = CalculateMovement();
        Vector3 horizontal = new Vector3(movement.x, 0, movement.z) * _speed;

   
        _playerVelocity.y += stateMachine.Gravity * deltaTime;
        Vector3 finalVelocity = horizontal;
        finalVelocity.y = _playerVelocity.y;

        stateMachine.Controller.Move(finalVelocity * deltaTime);

        if (stateMachine.IsGrounded() && _playerVelocity.y <= 0f)
        {
            stateMachine.SwitchState(new PlayerIdleState(stateMachine));
        }
    }

    public override void Exit()
    {
        
    }
}
