using System;
using UnityEngine;

public class WeaponBlockState : WeaponBaseState
{
    private int _blocksLeft;
    private float _parryWindowLeft;

    public WeaponBlockState(WeaponStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime("SwordBlock", 0.1f);
        stateMachine.Health.OnDamagedWhileBlocking += OnDamagedWhileBlocking;

        _blocksLeft = stateMachine.BlockAmount;
        _parryWindowLeft = stateMachine.ParryWindowAmount;
    }

    public override void Tick(float deltaTime)
    {
        if (!stateMachine.InputReader.BlockInput)
        {
            stateMachine.SwitchState(new WeaponIdleState(stateMachine));
        }

        _parryWindowLeft -= deltaTime;
    }

    public override void Exit()
    {
        stateMachine.Health.OnDamagedWhileBlocking -= OnDamagedWhileBlocking;
    }

    private void OnDamagedWhileBlocking(GameObject source)
    {
        _blocksLeft--;
        Debug.Log($"Blocks left: {_blocksLeft}");

        // Going to need a "break block" animation here eventually, but for now just switch back to idle when the block is broken
        if (_blocksLeft <= 0)
        {
            stateMachine.SwitchState(new WeaponIdleState(stateMachine));
        }

        if (_parryWindowLeft > 0)
        {
            Debug.Log("Parry!");
            //source.OnParried?.Invoke();
            EnemyStateMachine enemy = source.GetComponent<EnemyStateMachine>();

            if (!enemy) { return; }

            enemy.OnParried();

            //stateMachine.OnParry?.Invoke();
        }
    }
}
