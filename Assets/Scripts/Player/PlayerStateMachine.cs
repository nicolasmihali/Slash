using UnityEngine;
using static Unity.VisualScripting.Member;

public class PlayerStateMachine : StateMachine
{
    [field: SerializeField] public InputReader InputReader { get; private set; }
    [field: SerializeField] public CharacterController Controller { get; private set; }
    [field: SerializeField] public InteractionComponent InteractionComponent { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public CameraEffects CameraEffects { get; private set; }

    public PostureStrategy Posture { get; set; } = new StandingStrategy();

    [SerializeField] public float WalkSpeed;
    [SerializeField] public float SprintSpeed;
    [SerializeField] public float CrouchSpeed;
    [SerializeField] public float JumpHeight = 3f;
    [SerializeField] public float Gravity = -9.81f;
    [SerializeField] public float FallMultiplier = 4f;
    //[SerializeField] public float CrouchSpeedModifier = 0.5f;
    
    public float JumpForce => Mathf.Sqrt(JumpHeight * -3f * Gravity);

    public Transform MainCameraTransform {  get; private set; }

    private void Start()
    {
        MainCameraTransform = Camera.main.transform;

        Health.OnDamagedWithSource += OnDamaged;

        SwitchState(new PlayerIdleState(this));
    }

    private void OnDestroy()
    {
        Health.OnDamagedWithSource -= OnDamaged;
    }

    private void OnDamaged(Vector3 source)
    {
        SwitchState(new PlayerTakeDamageState(this, source));
    }

    public void SetPosture(PostureStrategy newPosture)
    {
        Posture = newPosture;
    }
}
