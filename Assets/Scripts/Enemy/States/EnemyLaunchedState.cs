using UnityEngine;

public class EnemyLaunchedState : EnemyBaseState
{
    private float _verticalVelocity;
    private const float Gravity = -25f;

    public EnemyLaunchedState(EnemyStateMachine stateMachine, float launchForce) : base(stateMachine)
    {
        _verticalVelocity = launchForce;
    }

    public override void Enter()
    {
        stateMachine.Agent.enabled = false;
        stateMachine.Animator.CrossFadeInFixedTime("Hit", 0.1f);
    }

    public override void Tick(float deltaTime)
    {
        _verticalVelocity += Gravity * deltaTime;

        if (_verticalVelocity < 0f)
            _verticalVelocity = Mathf.Max(_verticalVelocity, -2f);

        stateMachine.Controller.Move(new Vector3(0, _verticalVelocity, 0) * deltaTime);

        if (stateMachine.Controller.isGrounded && _verticalVelocity < 0f)
        {
            stateMachine.Agent.enabled = true;
            stateMachine.SwitchState(new EnemyTakeDamageState(stateMachine));
        }
    }

    public override void Exit()
    {
        
    }
}
