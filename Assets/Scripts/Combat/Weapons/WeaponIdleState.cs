using UnityEngine;

public class WeaponIdleState : WeaponBaseState
{
    public WeaponIdleState(WeaponStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime("SwordIdle", 0.2f);

        stateMachine.InputReader.AttackEvent += OnAttack;
        stateMachine.InputReader.LaunchAttackEvent += OnLaunchAttack;
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.InputReader.BlockInput)
        {
            stateMachine.SwitchState(new WeaponBlockState(stateMachine));
        }
    }

    public override void Exit()
    {
        stateMachine.InputReader.AttackEvent -= OnAttack;
    }

    private void OnAttack()
    {
        stateMachine.SwitchState(new WeaponHitState(stateMachine, 0));
    }

    private void OnLaunchAttack()
    {
        if (!stateMachine.PlayerStateMachine.IsGrounded()) { return; }
        stateMachine.SwitchState(new WeaponHitState(stateMachine, stateMachine.LaunchAttack));
    }
}
