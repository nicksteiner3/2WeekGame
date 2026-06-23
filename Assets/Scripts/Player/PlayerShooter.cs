using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [Header("Fire")]
    [SerializeField] private float damagePerShot = 25f;
    [SerializeField] private float shotsPerSecond = 4f;
    [SerializeField] private float maxDistance = 40f;
    [SerializeField] private Transform muzzlePoint;
    [SerializeField] private LayerMask hitMask = ~0;

    [Header("Debug")]
    [SerializeField] private bool drawDebugRay = true;

    private float _nextShotTime;

    private void Update()
    {
        if (!Input.GetMouseButton(0))
        {
            return;
        }

        if (Time.time < _nextShotTime)
        {
            return;
        }

        Fire();
    }

    private void Fire()
    {
        _nextShotTime = Time.time + (1f / Mathf.Max(0.01f, shotsPerSecond));

        Vector3 origin;
        Vector3 direction;

        if (muzzlePoint != null)
        {
            origin = muzzlePoint.position;
            direction = muzzlePoint.forward;
        }
        else
        {
            origin = transform.position + Vector3.up * 0.8f + transform.forward * 0.6f;
            direction = transform.forward;
        }

        if (Physics.Raycast(origin, direction, out RaycastHit hit, maxDistance, hitMask, QueryTriggerInteraction.Ignore))
        {
            IDamageable damageable = hit.collider.GetComponentInParent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(damagePerShot, gameObject);
            }

            if (drawDebugRay)
            {
                Debug.DrawLine(origin, hit.point, Color.red, 0.15f);
            }
        }
        else if (drawDebugRay)
        {
            Debug.DrawRay(origin, direction * maxDistance, Color.yellow, 0.15f);
        }
    }
}
