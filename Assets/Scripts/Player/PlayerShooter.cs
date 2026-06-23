using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [Header("Weapons")]
    [SerializeField] private WeaponData[] weapons;
    [SerializeField] private Transform muzzlePoint;
    [SerializeField] private LayerMask hitMask = ~0;

    [Header("Debug")]
    [SerializeField] private bool drawDebugRay = true;

    private int _activeWeaponIndex;
    private float _nextShotTime;

    public WeaponData ActiveWeapon => (weapons != null && weapons.Length > 0) ? weapons[_activeWeaponIndex] : null;

    private void Update()
    {
        HandleWeaponSwitch();
        HandleFiringInput();
    }

    private void HandleWeaponSwitch()
    {
        if (weapons == null || weapons.Length == 0)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) SetWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SetWeapon(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SetWeapon(2);

        float scroll = Input.GetAxisRaw("Mouse ScrollWheel");
        if (scroll > 0f)
        {
            SetWeapon((_activeWeaponIndex + 1) % weapons.Length);
        }
        else if (scroll < 0f)
        {
            SetWeapon((_activeWeaponIndex - 1 + weapons.Length) % weapons.Length);
        }
    }

    private void SetWeapon(int index)
    {
        if (weapons == null || index >= weapons.Length || weapons[index] == null)
        {
            return;
        }

        _activeWeaponIndex = index;
        _nextShotTime = 0f;
        Debug.Log($"[PlayerShooter] Switched to: {weapons[_activeWeaponIndex].weaponName}");
    }

    private void HandleFiringInput()
    {
        if (ActiveWeapon == null)
        {
            return;
        }

        bool fireInput = ActiveWeapon.isAutomatic
            ? Input.GetMouseButton(0)
            : Input.GetMouseButtonDown(0);

        if (fireInput && Time.time >= _nextShotTime)
        {
            Fire();
        }
    }

    private void Fire()
    {
        WeaponData weapon = ActiveWeapon;
        _nextShotTime = Time.time + (1f / Mathf.Max(0.01f, weapon.shotsPerSecond));

        Vector3 origin = muzzlePoint != null
            ? muzzlePoint.position
            : transform.position + Vector3.up * 0.8f + transform.forward * 0.6f;

        Vector3 baseDirection = muzzlePoint != null
            ? muzzlePoint.forward
            : transform.forward;

        for (int i = 0; i < weapon.pelletsPerShot; i++)
        {
            Vector3 direction = ApplySpread(baseDirection, weapon.spreadAngleDegrees);

            if (Physics.Raycast(origin, direction, out RaycastHit hit, weapon.maxDistance, hitMask, QueryTriggerInteraction.Ignore))
            {
                IDamageable damageable = hit.collider.GetComponentInParent<IDamageable>();
                if (damageable != null)
                {
                    damageable.TakeDamage(weapon.damagePerPellet, gameObject);
                }

                if (drawDebugRay)
                {
                    Debug.DrawLine(origin, hit.point, Color.red, 0.15f);
                }
            }
            else if (drawDebugRay)
            {
                Debug.DrawRay(origin, direction * weapon.maxDistance, Color.yellow, 0.15f);
            }
        }
    }

    private Vector3 ApplySpread(Vector3 direction, float spreadDegrees)
    {
        if (spreadDegrees <= 0f)
        {
            return direction;
        }

        float halfAngle = spreadDegrees * 0.5f;
        direction = Quaternion.AngleAxis(Random.Range(-halfAngle, halfAngle), Vector3.up) * direction;
        direction = Quaternion.AngleAxis(Random.Range(-halfAngle, halfAngle), transform.right) * direction;
        return direction.normalized;
    }
}
