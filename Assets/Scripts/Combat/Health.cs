using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100f;

    public event Action OnDamaged;
    private float health;

    private void Start()
    {
        health = _maxHealth;
    }

    public void DealDamage(float damage)
    {
        if (health == 0) {
            Destroy(gameObject);
        }

        health = Mathf.Max(0, health - damage);
        OnDamaged?.Invoke();
        // Death check and such.

        Debug.Log(health);
    }

    public float GetHealth()
    {
        return health;
    }
}
