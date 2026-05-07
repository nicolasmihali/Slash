using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : StateMachine
{
    [field: SerializeField] public CharacterController Controller { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public GameObject Player { get; private set; }
    [field: SerializeField] public NavMeshAgent Agent { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public WeaponHandler WeaponHandler { get; private set; }
    [field: SerializeField] public WeaponDamage WeaponDamage { get; private set; }
    [field: SerializeField] public float MovementSpeed { get; private set; }
    [field: SerializeField] public float AttackDistance { get; private set; }
    [field: SerializeField] public Attack[] Attacks { get; private set; }

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        Agent.updatePosition = false;
        Agent.updateRotation = false;

        Health.OnDamaged += OnDamaged;

        SwitchState(new EnemyIdleState(this));
    }

    public void LookAtPlayer(float deltaTime)
    {
        Vector3 direction = Player.transform.position - transform.position;
        direction.y = 0f;

        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, deltaTime * 10f);
    }

    public void OnDestroy()
    {
        Health.OnDamaged -= OnDamaged;
    }

    public void EnableWeapon()
    {
        WeaponHandler.EnableWeapon();
    }

    public void DisableWeapon()
    {
        WeaponHandler.DisableWeapon();
    }

    private void OnDamaged()
    {
        SwitchState(new EnemyTakeDamageState(this));
    }
}
