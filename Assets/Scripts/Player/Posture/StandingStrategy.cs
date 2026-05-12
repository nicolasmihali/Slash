using UnityEngine;

public class StandingStrategy : PostureStrategy
{
    public void SetPosture(PlayerStateMachine stateMachine, float deltaTime)
    {
        float previousHeight = stateMachine.Controller.height;
        stateMachine.Controller.height = Mathf.Lerp(previousHeight, 2.0f, deltaTime * 10f);

        float heightDifference = stateMachine.Controller.height - previousHeight;
        stateMachine.Controller.Move(new Vector3(0, -heightDifference / 2f, 0));
    }

    public float GetHeight() => 2.0f;
}
