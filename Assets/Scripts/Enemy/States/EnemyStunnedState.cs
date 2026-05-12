using UnityEngine;

public class EnemyStunnedState : EnemyBaseState
{
    public EnemyStunnedState(EnemyStateMachine stateMachine) : base(stateMachine)
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
            stateMachine.SwitchState(new EnemyChaseState(stateMachine));
        }
    }

    public override void Exit()
    {

    }
}
