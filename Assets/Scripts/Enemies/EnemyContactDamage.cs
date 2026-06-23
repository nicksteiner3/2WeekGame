using UnityEngine;

public class EnemyContactDamage : MonoBehaviour
{
    [SerializeField] private float damagePerTick = 10f;
    [SerializeField] private float damageInterval = 0.5f;

    private float _nextDamageTime;

    private void OnCollisionStay(Collision collision)
    {
        EnemyChaser chaser = GetComponent<EnemyChaser>();
        if (chaser != null && chaser.freezeMovement)
        {
            return;
        }

        if (Time.time < _nextDamageTime)
        {
            return;
        }

        PlayerHealth playerHealth = collision.collider.GetComponentInParent<PlayerHealth>();
        if (playerHealth == null)
        {
            return;
        }

        playerHealth.TakeDamage(damagePerTick, gameObject);
        _nextDamageTime = Time.time + Mathf.Max(0.05f, damageInterval);
    }
}
