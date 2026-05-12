using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    private Attack _attack;

    public EnemyAttackState(EnemyStateMachine stateMachine, int attackIndex) : base(stateMachine)
    {
        _attack = stateMachine.Attacks[attackIndex];
        //stateMachine.OnParried += OnParried;
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(_attack.AnimationName, _attack.TransitionDuration);

        stateMachine.WeaponDamage.SetAttack(_attack.Damage);
    }

    public override void Tick(float deltaTime)
    {
        float distance = Vector3.Distance(stateMachine.transform.position, stateMachine.Player.transform.position);

        float normalizedTime = GetNormalizedTime("Attack");

        if (normalizedTime >= 1f)
        {
            // Temporary code
            Vector3 direction = stateMachine.Player.transform.position - stateMachine.transform.position;
            direction.y = 0f;
            if (direction != Vector3.zero)
                stateMachine.transform.rotation = Quaternion.LookRotation(direction);

            // If player is not in range, chase.
            if (distance > stateMachine.AttackDistance)
            {
                stateMachine.SwitchState(new EnemyChaseState(stateMachine));
                return;
            }

            TryComboAttack();
        }

    }

    public override void Exit()
    {

    }

    private void TryComboAttack()
    {
        float normalizedTime = GetNormalizedTime("Attack");

        if (_attack.ComboStateIndex == -1) { return; }

        if (normalizedTime < 0.5) { return; }

        stateMachine.SwitchState(new EnemyAttackState(stateMachine, _attack.ComboStateIndex));
    }
    
    
}
