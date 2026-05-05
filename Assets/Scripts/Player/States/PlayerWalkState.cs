using UnityEngine;

public class PlayerWalkState : PlayerBaseState
{
    public PlayerWalkState(PlayerStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter()
    {
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.InputReader.MovementValue.magnitude <= 0)
        {
            stateMachine.SwitchState(new PlayerIdleState(stateMachine));
        }

        else if (stateMachine.InputReader.SprintInput)
        {
            stateMachine.SwitchState(new PlayerSprintState(stateMachine));
        }

        Vector3 movement = CalculateMovement();
        stateMachine.Controller.Move(movement * stateMachine.WalkSpeed * deltaTime);
    }

    public override void Exit()
    {
        
    }
}
