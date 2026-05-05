using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttackState : EnemyBaseState
{
    private Attack _attack;

    [SerializeField] private float attackDistance;

    public EnemyAttackState(EnemyStateMachine stateMachine, int attackIndex) : base(stateMachine)
    {
        _attack = stateMachine.Attacks[attackIndex];
    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(_attack.AnimationName, _attack.TransitionDuration);
    }

    public override void Tick(float deltaTime)
    {
        float distance = Vector3.Distance(stateMachine.transform.position, stateMachine.Player.transform.position);

        float normalizedTime = GetNormalizedTime();

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

        if (normalizedTime < 0.5) { return; }

        stateMachine.SwitchState(new EnemyAttackState(stateMachine, _attack.ComboStateIndex));
    }
}
