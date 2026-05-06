using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    private int LocomotionBlendTreeHash = Animator.StringToHash("LocomotionBlendTree");
    private int SpeedHash = Animator.StringToHash("Speed");

    private const float AnimatorDampValue = 0.1f;
    private const float CrossFadeDuration = 0.1f;

    public EnemyChaseState(EnemyStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(LocomotionBlendTreeHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        float distance = Vector3.Distance(stateMachine.transform.position, stateMachine.Player.transform.position);
        Debug.Log(distance);
        if (distance <= stateMachine.AttackDistance)
        {
            stateMachine.SwitchState(new EnemyAttackState(stateMachine, 0));
        }

        MoveToPlayer(deltaTime);
        stateMachine.LookAtPlayer(deltaTime);

        //stateMachine.transform.LookAt(stateMachine.Player.transform);
        stateMachine.Animator.SetFloat(SpeedHash, 1f, AnimatorDampValue, deltaTime);
    }

    public override void Exit()
    {
        stateMachine.Agent.ResetPath();
        stateMachine.Agent.velocity = Vector3.zero;
    }

    private void MoveToPlayer(float deltaTime)
    {
        stateMachine.Agent.destination = stateMachine.Player.transform.position;

        Move(stateMachine.Agent.desiredVelocity.normalized * stateMachine.MovementSpeed, deltaTime);

        stateMachine.Agent.velocity = stateMachine.Controller.velocity;
        stateMachine.Agent.nextPosition = stateMachine.transform.position;
    }

    
}
