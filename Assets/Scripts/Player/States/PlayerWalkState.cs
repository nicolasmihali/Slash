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

        if (!stateMachine.IsGrounded())
        {
            stateMachine.SwitchState(new PlayerAirborneState(stateMachine, stateMachine.WalkSpeed, 0f));
        }

        float currentSpeed = GetMovementSpeed();
        if (stateMachine.Posture is CrouchingStrategy)
        {
            currentSpeed = stateMachine.CrouchSpeed;
        }

        Vector3 movement = CalculateMovement();
        Vector3 horizontal = new Vector3(movement.x, 0f, movement.z) * currentSpeed;

        // Small downward bias to keep grounded
        //horizontal.y = -0.1f;

        stateMachine.Controller.Move(horizontal * deltaTime);
        /*Vector3 movement = CalculateMovement();
        movement.y = Physics.gravity.y;

        stateMachine.Controller.Move(movement * currentSpeed * deltaTime);*/
    }

    public override void Exit()
    {
        base.Exit();
    }

    protected override float GetMovementSpeed() => stateMachine.WalkSpeed;
}
