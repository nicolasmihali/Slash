using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine stateMachine;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    protected Vector3 CalculateMovement()
    {
        Vector3 forward = stateMachine.MainCameraTransform.forward;
        forward.y = 0f;
        forward.Normalize();

        Vector3 right = stateMachine.MainCameraTransform.right;
        right.y = 0f;
        right.Normalize();

        return forward * stateMachine.InputReader.MovementValue.y + right * stateMachine.InputReader.MovementValue.x;
    }

    protected void OnInteract()
    {
        stateMachine.InteractionComponent.hoveredInteractable?.Interact();
    }
}
