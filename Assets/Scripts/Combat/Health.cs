using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100f;

    public event Action OnDamaged;
    public event Action<Vector3> OnDamagedWithSource;

    public event Action<GameObject> OnDamagedWhileBlocking;
    private float health;

    private void Start()
    {
        health = _maxHealth;
    }

    public void DealDamage(float damage, GameObject source, Vector3 sourceTransform)
    {
        if (gameObject.TryGetComponent<WeaponStateMachine>(out WeaponStateMachine weaponStateMachine) && weaponStateMachine.CurrentStateName == "WeaponBlockState")
        {
            OnDamagedWhileBlocking?.Invoke(source);
            return;
        }
        health = Mathf.Max(0, health - damage);

        if (gameObject.CompareTag("Player")) { 
            OnDamagedWithSource?.Invoke(sourceTransform);
        }
        else { OnDamaged?.Invoke(); }

        if (health <= 0) { Destroy(gameObject); }
    }

    public float GetHealth()
    {
        return health;
    }
}
