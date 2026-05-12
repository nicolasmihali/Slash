using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(PlayerStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter()
    {
        base.Enter();
    }

    public override void Tick(float deltaTime)
    {
        base.Tick(deltaTime);

        /*Vector3 movement = CalculateMovement();
        movement.y = Physics.gravity.y;

        stateMachine.Controller.Move(movement * deltaTime);*/

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

        if (stateMachine.InputReader.MovementValue.magnitude > 0.1)
        {
            stateMachine.SwitchState(new PlayerWalkState(stateMachine));
        }

        
    }

    public override void Exit()
    {
        base.Exit();
    }

    protected override float GetMovementSpeed() => stateMachine.WalkSpeed;
}
