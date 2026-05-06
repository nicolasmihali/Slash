using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHitState : WeaponBaseState
{
    private Attack _attack;

    public WeaponHitState(WeaponStateMachine stateMachine, int attackIndex) : base(stateMachine)
    {
        _attack = stateMachine.Attacks[attackIndex];

        stateMachine.InputReader.AttackEvent += TryComboAttack;
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(_attack.AnimationName, _attack.TransitionDuration);

        stateMachine.WeaponDamage.SetAttack(_attack.Damage);
    }

    public override void Tick(float deltaTime)
    {
        float normalizedTime = GetNormalizedTime();

        if (normalizedTime >= 1f)
        {
            stateMachine.SwitchState(new WeaponIdleState(stateMachine));
        }
    }

    private float GetNormalizedTime()
    {
        AnimatorStateInfo currentInfo = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = stateMachine.Animator.GetNextAnimatorStateInfo(0);

        if (stateMachine.Animator.IsInTransition(0) && nextInfo.IsTag("Attack"))
        {
            return nextInfo.normalizedTime;
        }

        else if (!stateMachine.Animator.IsInTransition(0) && currentInfo.IsTag("Attack"))
        {
            return currentInfo.normalizedTime;
        }

        return 0;
    }

    private void TryComboAttack()
    {
        float normalizedTime = GetNormalizedTime();

        if (_attack.ComboStateIndex == -1) { return; }

        if (normalizedTime < _attack.ComboAttackTime) { return; }

        stateMachine.SwitchState(new WeaponHitState(stateMachine, _attack.ComboStateIndex));
    }

    public override void Exit()
    {
        stateMachine.InputReader.AttackEvent -= TryComboAttack;
    }
}
