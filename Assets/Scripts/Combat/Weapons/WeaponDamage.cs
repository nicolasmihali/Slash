using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] private Collider _userCollider;
    [SerializeField] private GameObject _user;

    public event Action<EnemyStateMachine, float> OnLaunchHit;

    private List<Collider> _alreadyCollidedWith = new List<Collider>();
    private Attack _attack;

    private void OnEnable()
    {
        _alreadyCollidedWith.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == _userCollider) { return; }

        if (_alreadyCollidedWith.Contains(other) ) { return; }

        _alreadyCollidedWith.Add(other);

        if (other.TryGetComponent<Health>(out Health health))
        {
            if (other.CompareTag("Enemy") && _userCollider.CompareTag("Player"))
            {
                Debug.Log("Enemy health: " + health.GetHealth());
            }
            health.DealDamage(_attack.Damage, _user, transform.position);
        }

        if (_attack.IsLauncher && other.TryGetComponent<EnemyStateMachine>(out var enemy))
        {
            Debug.Log("Launch hit detected, invoking OnLaunchHit");
            OnLaunchHit?.Invoke(enemy, _attack.LaunchForce);
        }
    }

    public void SetAttack(Attack attack)
    {
        _attack = attack;

    }
}
