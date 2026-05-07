using UnityEngine;

public class StandingStrategy : PostureStrategy
{
    public void SetPosture(PlayerStateMachine stateMachine, float deltaTime)
    {
        stateMachine.Controller.height = Mathf.Lerp(stateMachine.Controller.height, 2.0f, deltaTime * 10f);
    }

    public float GetHeight() => 2.0f;
}
