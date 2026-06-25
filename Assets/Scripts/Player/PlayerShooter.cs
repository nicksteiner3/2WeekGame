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

    private int[] _currentAmmo;
    private bool[] _isReloading;
    private float[] _reloadEndTime;

    public WeaponData ActiveWeapon => (weapons != null && weapons.Length > 0) ? weapons[_activeWeaponIndex] : null;
    public int ActiveAmmo => _currentAmmo != null ? _currentAmmo[_activeWeaponIndex] : 0;
    public bool IsReloading => _isReloading != null && _isReloading[_activeWeaponIndex];

    private void Awake()
    {
        if (weapons == null)
        {
            return;
        }

        _currentAmmo = new int[weapons.Length];
        _isReloading = new bool[weapons.Length];
        _reloadEndTime = new float[weapons.Length];

        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i] != null)
            {
                _currentAmmo[i] = weapons[i].magazineSize;
            }
        }
    }

    private void Update()
    {
        HandleWeaponSwitch();
        HandleReloadInput();
        HandleFiringInput();
        CheckReloadComplete();
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

    private void HandleReloadInput()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            TryStartReload();
        }
    }

    private void HandleFiringInput()
    {
        if (ActiveWeapon == null || IsReloading)
        {
            return;
        }

        bool fireInput = ActiveWeapon.isAutomatic
            ? Input.GetMouseButton(0)
            : Input.GetMouseButtonDown(0);

        if (!fireInput || Time.time < _nextShotTime)
        {
            return;
        }

        if (_currentAmmo[_activeWeaponIndex] <= 0)
        {
            TryStartReload();
            return;
        }

        Fire();
    }

    private void Fire()
    {
        WeaponData weapon = ActiveWeapon;
        
        // Apply fire rate upgrade multiplier
        float fireRateMultiplier = UpgradeManager.Instance != null 
            ? UpgradeManager.Instance.GetFireRateMultiplier() 
            : 1f;
        _nextShotTime = Time.time + (1f / Mathf.Max(0.01f, weapon.shotsPerSecond * fireRateMultiplier));
        _currentAmmo[_activeWeaponIndex]--;

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
                    // Apply damage upgrade multiplier
                    float damageMultiplier = UpgradeManager.Instance != null 
                        ? UpgradeManager.Instance.GetDamageMultiplier() 
                        : 1f;
                    float finalDamage = weapon.damagePerPellet * damageMultiplier;
                    damageable.TakeDamage(finalDamage, gameObject);
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

    private void TryStartReload()
    {
        if (ActiveWeapon == null || _isReloading[_activeWeaponIndex])
        {
            return;
        }

        if (_currentAmmo[_activeWeaponIndex] >= ActiveWeapon.magazineSize)
        {
            return;
        }

        _isReloading[_activeWeaponIndex] = true;
        _reloadEndTime[_activeWeaponIndex] = Time.time + ActiveWeapon.reloadTime;
        Debug.Log($"[PlayerShooter] Reloading {ActiveWeapon.weaponName}...");
    }

    private void CheckReloadComplete()
    {
        if (_isReloading == null)
        {
            return;
        }

        for (int i = 0; i < _isReloading.Length; i++)
        {
            if (_isReloading[i] && Time.time >= _reloadEndTime[i])
            {
                _isReloading[i] = false;
                _currentAmmo[i] = weapons[i] != null ? weapons[i].magazineSize : 0;
                Debug.Log($"[PlayerShooter] {weapons[i].weaponName} reloaded.");
            }
        }
    }

    public void RefillAllAmmo()
    {
        if (_currentAmmo == null || weapons == null)
        {
            return;
        }

        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i] != null)
            {
                _currentAmmo[i] = weapons[i].magazineSize;
                _isReloading[i] = false;
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
