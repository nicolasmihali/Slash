using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerWalkState : PlayerGroundedState
{
    public PlayerWalkState(PlayerStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter()
    {
        base.Enter();
    }

    public override void Tick(float deltaTime)
    {
        base.Tick(deltaTime);

        if (stateMachine.InputReader.MovementValue.magnitude <= 0)
        {
            stateMachine.SwitchState(new PlayerIdleState(stateMachine));
        }

        else if (stateMachine.InputReader.SprintInput)
        {
            stateMachine.SwitchState(new PlayerSprintState(stateMachine));
        }

        if (!stateMachine.Controller.isGrounded)
        {
            stateMachine.SwitchState(new PlayerAirborneState(stateMachine, stateMachine.WalkSpeed, 0f));
        }

        float currentSpeed = GetMovementSpeed();
        if (stateMachine.Posture is CrouchingStrategy)
        {
            currentSpeed = stateMachine.WalkSpeed * 0.5f;
        }

        Vector3 movement = CalculateMovement();
        movement.y = Physics.gravity.y;

        stateMachine.Controller.Move(movement * currentSpeed * deltaTime);
    }

    public override void Exit()
    {
        base.Exit();
    }

    protected override float GetMovementSpeed() => stateMachine.WalkSpeed;
}
