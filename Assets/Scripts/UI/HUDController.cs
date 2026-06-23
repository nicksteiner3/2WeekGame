using TMPro;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerShooter playerShooter;
    [SerializeField] private PlayerHealth playerHealth;

    [Header("Ammo")]
    [SerializeField] private TextMeshProUGUI ammoText;

    [Header("Health")]
    [SerializeField] private TextMeshProUGUI healthText;

    private void Start()
    {
        if (playerShooter == null)
        {
            playerShooter = FindObjectOfType<PlayerShooter>();
        }

        if (playerHealth == null)
        {
            playerHealth = FindObjectOfType<PlayerHealth>();
        }
    }

    private void Update()
    {
        UpdateAmmoDisplay();
        UpdateHealthDisplay();
    }

    private void UpdateAmmoDisplay()
    {
        if (ammoText == null || playerShooter == null)
        {
            return;
        }

        WeaponData weapon = playerShooter.ActiveWeapon;
        if (weapon == null)
        {
            ammoText.text = "--/--";
            return;
        }

        if (playerShooter.IsReloading)
        {
            ammoText.text = $"{weapon.weaponName}\nRELOADING...";
        }
        else
        {
            ammoText.text = $"{weapon.weaponName}\n{playerShooter.ActiveAmmo} / {weapon.magazineSize}";
        }
    }

    private void UpdateHealthDisplay()
    {
        if (healthText == null || playerHealth == null)
        {
            return;
        }

        healthText.text = $"HP: {Mathf.CeilToInt(playerHealth.CurrentHealth)}";
    }
}
