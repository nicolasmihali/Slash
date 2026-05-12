using UnityEngine;

public class PlayerSprintState : PlayerGroundedState
{
    
    public PlayerSprintState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
       
    }

    public override void Enter()
    {
        base.Enter();
        //stateMachine.InputReader.JumpEvent += OnJump;
    }

    public override void Tick(float deltaTime)
    {
        base.Tick(deltaTime);

        if (!stateMachine.InputReader.SprintInput)
        {
            stateMachine.SwitchState(new PlayerWalkState(stateMachine));
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
    }

    public override void Exit()
    {
        base.Exit();
        //stateMachine.InputReader.JumpEvent -= OnJump;
    }

    protected override float GetMovementSpeed() => stateMachine.SprintSpeed;
}
