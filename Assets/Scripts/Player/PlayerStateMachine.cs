using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field: SerializeField] public InputReader InputReader { get; private set; }
    [field: SerializeField] public CharacterController Controller { get; private set; }
    [field: SerializeField] public InteractionComponent InteractionComponent { get; private set; }
    [field: SerializeField] public Health health { get; private set; }

    [SerializeField] public float WalkSpeed;
    [SerializeField] public float SprintSpeed;

    public Transform MainCameraTransform {  get; private set; }

    private void Start()
    {
        MainCameraTransform = Camera.main.transform;

        health.OnDamaged += () => SwitchState(new PlayerHitState(this));

        SwitchState(new PlayerIdleState(this));
    }

    private void OnDestroy()
    {
        health.OnDamaged -= () => SwitchState(new PlayerHitState(this));
    }
}
