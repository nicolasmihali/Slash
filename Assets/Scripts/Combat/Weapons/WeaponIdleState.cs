using UnityEngine;

public class WeaponIdleState : WeaponBaseState
{
    public WeaponIdleState(WeaponStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.InputReader.AttackEvent += OnAttack;
        stateMachine.Animator.Play("Idle");
    }

    public override void Tick(float deltaTime)
    {
        
    }

    public override void Exit()
    {
        stateMachine.InputReader.AttackEvent -= OnAttack;
    }

    private void OnAttack()
    {
        stateMachine.SwitchState(new WeaponHitState(stateMachine, 0));
    }
}
