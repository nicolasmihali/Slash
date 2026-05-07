using Unity.VisualScripting;
using UnityEngine;

public class EnemyTakeDamageState : EnemyBaseState
{
    public EnemyTakeDamageState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime("Hit", 0.3f);
    }

    public override void Tick(float deltaTime)
    {
        float normalizedTime = GetNormalizedTime("Hit");

        if (normalizedTime >= 1f)
        {
            Debug.Log("hit anim finishsed");
            stateMachine.SwitchState(new EnemyChaseState(stateMachine));
        }
    }

    public override void Exit()
    {
        
    }
}
