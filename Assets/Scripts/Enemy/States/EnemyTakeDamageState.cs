using Unity.VisualScripting;
using UnityEngine;

public class EnemyTakeDamageState : EnemyBaseState
{
    private const string AnimationName = "Hit";
    private const float CrossFadeDuration = 0.1f;
    private const float AttackForce = 5f;
    public EnemyTakeDamageState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(AnimationName, CrossFadeDuration);

        Vector3 knockbackDir = (stateMachine.transform.position - stateMachine.Player.transform.position).normalized;
        stateMachine.ForceReceiver.AddForce(knockbackDir * AttackForce);
    }

    public override void Tick(float deltaTime)
    {
        //TryApplyForce();

        float normalizedTime = GetNormalizedTime(AnimationName);

        if (normalizedTime >= 1f)
        {
            stateMachine.SwitchState(new EnemyChaseState(stateMachine));
        }

        stateMachine.Controller.Move(stateMachine.ForceReceiver.Movement * deltaTime);
    }

    public override void Exit()
    {
        
    }
}
