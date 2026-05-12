using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] private Collider _userCollider;
    [SerializeField] private GameObject _user;

    private List<Collider> _alreadyCollidedWith = new List<Collider>();
    private float _damage;

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
                
            }
            health.DealDamage(_damage, _user, transform.position);
        }
    }

    public void SetAttack(float damage)
    {
        _damage = damage;
    }
}
