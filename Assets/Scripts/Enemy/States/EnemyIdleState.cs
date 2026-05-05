using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class EnemyIdleState : EnemyBaseState
{
    private int SpeedHash = Animator.StringToHash("Speed");
    private int LocomotionBlendTreeHash = Animator.StringToHash("LocomotionBlendTree");

    private const float AnimatorDampValue = 0.1f;
    private const float CrossFadeDuration = 0.1f;

    public EnemyIdleState(EnemyStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(LocomotionBlendTreeHash, CrossFadeDuration);
        Debug.Log("idle state");
    }

    public override void Tick(float deltaTime)
    {
        Move(Vector3.zero, deltaTime);

        stateMachine.Animator.SetFloat(SpeedHash, 0f, AnimatorDampValue, deltaTime);

        float distance = Vector3.Distance(stateMachine.transform.position, stateMachine.Player.transform.position);

        
        if (distance < 10)
        {
            stateMachine.SwitchState(new EnemyChaseState(stateMachine));
        }
    }

    public override void Exit()
    {

    }
}
