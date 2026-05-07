using UnityEngine;

public class CrouchingStrategy : PostureStrategy
{
    public void SetPosture(PlayerStateMachine stateMachine, float deltaTime)
    {
        stateMachine.Controller.height = Mathf.Lerp(stateMachine.Controller.height, 1.0f, deltaTime * 10f);
    }

    public float GetHeight() => 1.0f;
}
