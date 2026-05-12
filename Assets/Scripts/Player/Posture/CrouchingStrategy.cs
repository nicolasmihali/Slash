using UnityEngine;

public class CrouchingStrategy : PostureStrategy
{
    public void SetPosture(PlayerStateMachine stateMachine, float deltaTime)
    {
        float previousHeight = stateMachine.Controller.height;
        stateMachine.Controller.height = Mathf.Lerp(previousHeight, 1.0f, deltaTime * 10f);

        float heightDifference = previousHeight - stateMachine.Controller.height;
        stateMachine.Controller.Move(new Vector3(0, -heightDifference / 2f, 0));
    }

    public float GetHeight() => 1.0f;
}
