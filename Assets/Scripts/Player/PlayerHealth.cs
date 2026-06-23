using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 100f;

    public float CurrentHealth { get; private set; }
    public bool IsDead => CurrentHealth <= 0f;

    public delegate void HealthEvent();
    public event HealthEvent OnDeath;

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
            OnDeath?.Invoke();
            Debug.Log("Player died.");
        }
    }

    [ContextMenu("Restore Full Health")]
    public void RestoreFullHealth()
    {
        CurrentHealth = maxHealth;
    }
}
