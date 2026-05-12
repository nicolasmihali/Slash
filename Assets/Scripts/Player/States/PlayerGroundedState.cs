
using UnityEngine;

public abstract class PlayerGroundedState : PlayerBaseState
{
    public PlayerGroundedState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        stateMachine.InputReader.InteractEvent += OnInteract;
        stateMachine.InputReader.JumpEvent += OnJump;
    }

    public override void Tick(float deltaTime)
    {
        // If we step off a ledge, fall (0 vertical velocity)
        if (!stateMachine.IsGrounded())
        {
            stateMachine.SwitchState(new PlayerAirborneState(stateMachine, GetMovementSpeed(), 0f));
        }

        if (!stateMachine.InputReader.CrouchInput && stateMachine.Posture is CrouchingStrategy)
        {
            stateMachine.SetPosture(new StandingStrategy());
        }
        else if (stateMachine.InputReader.CrouchInput && stateMachine.Posture is StandingStrategy)
        {
            stateMachine.SetPosture(new CrouchingStrategy());
        }

        stateMachine.Posture.SetPosture(stateMachine, deltaTime);

        stateMachine.Controller.Move(Vector3.down * 2f * deltaTime);
    }

    public override void Exit()
    {
        stateMachine.InputReader.InteractEvent -= OnInteract;
        stateMachine.InputReader.JumpEvent -= OnJump;
    }

    private void OnJump()
    {
        // Can't jump while crouching
        if (stateMachine.Posture is CrouchingStrategy) { return; }
        stateMachine.SwitchState(new PlayerAirborneState(stateMachine, GetMovementSpeed(), stateMachine.JumpForce));
    }

    // Forces the child states (Idle, Walk, Sprint) to define their speed
    protected abstract float GetMovementSpeed();
}