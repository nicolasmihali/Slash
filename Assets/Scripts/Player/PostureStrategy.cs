using UnityEngine;

public interface PostureStrategy
{
    void SetPosture(PlayerStateMachine stateMachine, float deltaTime);
    float GetHeight();
    

}
