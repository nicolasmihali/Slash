using System;
using UnityEngine;
using static Unity.VisualScripting.Member;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerStateMachine : StateMachine
{
    [field: SerializeField] public InputReader InputReader { get; private set; }
    [field: SerializeField] public CharacterController Controller { get; private set; }
    [field: SerializeField] public InteractionComponent InteractionComponent { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public CameraEffects CameraEffects { get; private set; }
    [field: SerializeField] public LayerMask CrouchLayerMask { get; private set; }
    [field: SerializeField] public WeaponDamage WeaponDamage { get; private set; }

    public PostureStrategy Posture { get; set; } = new StandingStrategy();

    [SerializeField] public float WalkSpeed;
    [SerializeField] public float SprintSpeed;
    [SerializeField] public float CrouchSpeed = 2f;
    [SerializeField] public float JumpHeight = 3f;
    [SerializeField] public float Gravity;
    [SerializeField] public float FallMultiplier = 4f;
    //[SerializeField] public float CrouchSpeedModifier = 0.5f;
    
    public float JumpForce => Mathf.Sqrt(JumpHeight * -3f * Gravity);

    public Transform MainCameraTransform {  get; private set; }

    private void Start()
    {
        MainCameraTransform = Camera.main.transform;

        Health.OnDamagedWithSource += OnDamaged;
        WeaponDamage.OnLaunchHit += OnLaunchHit;

        SwitchState(new PlayerIdleState(this));
    }

    private void OnDestroy()
    {
        Health.OnDamagedWithSource -= OnDamaged;
        WeaponDamage.OnLaunchHit -= OnLaunchHit;
    }

    public bool IsGrounded()
    {
        float maxDistance = Controller.bounds.extents.y;
        return Physics.SphereCast(Controller.bounds.center, 0.5f, Vector3.down, out RaycastHit hitInfo, maxDistance, CrouchLayerMask);

    }

    private void OnDamaged(Vector3 source)
    {
        SwitchState(new PlayerTakeDamageState(this, source));
    }

    private void OnLaunchHit(EnemyStateMachine enemy, float launchForce)
    {
        Debug.Log("OnLaunchHit received");
        enemy.SwitchState(new EnemyLaunchedState(enemy, launchForce));
        SwitchState(new PlayerAerialAttackState(this, launchForce));
    }

    public void SetPosture(PostureStrategy newPosture)
    {
        Posture = newPosture;
    }
}
