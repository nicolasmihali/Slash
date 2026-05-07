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

        Vector3 movement = CalculateMovement();
        movement.y = Physics.gravity.y;

        stateMachine.Controller.Move(movement * deltaTime);

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
