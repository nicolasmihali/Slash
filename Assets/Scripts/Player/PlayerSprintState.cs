using UnityEngine;

public class PlayerSprintState : PlayerBaseState
{
    public PlayerSprintState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
    }

    public override void Tick(float deltaTime)
    {
        if (!stateMachine.InputReader.SprintInput)
        {
            stateMachine.SwitchState(new PlayerWalkState(stateMachine));
        }

        Vector3 movement = CalculateMovement();
        stateMachine.Controller.Move(movement * stateMachine.SprintSpeed * deltaTime);
    }

    public override void Exit()
    {
        
    }
}
