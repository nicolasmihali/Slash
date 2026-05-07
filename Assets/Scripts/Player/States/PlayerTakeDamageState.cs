using UnityEngine;

public class PlayerTakeDamageState : PlayerBaseState
{
    private Vector3 _source;
    private float _duration = 0.3f;
    private float _timer;

    public PlayerTakeDamageState(PlayerStateMachine stateMachine, Vector3 source) : base(stateMachine)
    {
        _source = source;
    }

    public override void Enter()
    {
        Debug.Log("GOT HIT");
        //stateMachine.CameraEffects.AddDamageKick(_source);
    }

    public override void Tick(float deltaTime)
    {
        _timer += deltaTime;
        if (_timer >= _duration)
        {
            // Transition to another state after the damage state duration
            stateMachine.SwitchState(new PlayerIdleState(stateMachine));
        }
    }

    public override void Exit()
    {
        
    }
}
