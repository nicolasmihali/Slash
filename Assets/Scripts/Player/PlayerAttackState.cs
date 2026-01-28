using UnityEngine;

public class PlayerAttackState : PlayerBaseState
{
    public PlayerAttackState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("Player entered Attack state");
        //stateMachine.WeaponAnimator.Play("SwordSwing");
    }

    public override void Tick(float deltaTime)
    {
        
    }

    public override void Exit()
    {
        Debug.Log("Exiting Attack state");
    }

    private void GetNormalizedTime()
    {
        
    }
}
