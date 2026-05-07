using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100f;

    public event Action OnDamaged;
    public event Action<Vector3> OnDamagedWithSource;
    private float health;

    private void Start()
    {
        health = _maxHealth;
    }

    public void DealDamage(float damage, Vector3 source)
    {
        health = Mathf.Max(0, health - damage);
        if (gameObject.CompareTag("Player")) { OnDamagedWithSource?.Invoke(source); }
        else { OnDamaged?.Invoke(); }

        if (health <= 0) { Destroy(gameObject); }
    }

    public float GetHealth()
    {
        return health;
    }
}
