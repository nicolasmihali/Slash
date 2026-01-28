using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter()
    {
        stateMachine.InputReader.InteractEvent += OnInteract;
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.InputReader.MovementValue.magnitude > 0.1)
        {
            stateMachine.SwitchState(new PlayerWalkState(stateMachine));
        }
    }

    public override void Exit()
    {
    }
}
