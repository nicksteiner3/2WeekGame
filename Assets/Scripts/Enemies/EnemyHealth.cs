using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 50f;

    public float CurrentHealth { get; private set; }
    public bool IsDead => CurrentHealth <= 0f;

    private void Awake()
    {
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(float amount, GameObject source = null)
    {
        if (IsDead || amount <= 0f)
        {
            return;
        }

        CurrentHealth = Mathf.Max(0f, CurrentHealth - amount);

        if (IsDead)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
