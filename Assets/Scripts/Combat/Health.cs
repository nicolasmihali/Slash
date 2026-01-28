using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100f;

    private float health;

    private void Start()
    {
        health = _maxHealth;
    }

    public void DealDamage(float damage)
    {
        if (health == 0) { return; }

        health = Mathf.Max(0, health - damage);
        Debug.Log(health);
    }
}
