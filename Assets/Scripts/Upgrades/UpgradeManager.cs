using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance { get; private set; }

    [SerializeField] private UpgradeData[] upgrades = new UpgradeData[6];

    public enum UpgradeType
    {
        Damage = 0,
        FireRate = 1,
        MoveSpeed = 2,
        DashCharge = 3,
        ExtractionBonus = 4,
        PickupRadius = 5
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Validate upgrades array
        if (upgrades == null || upgrades.Length != 6)
        {
            Debug.LogWarning("[UpgradeManager] Upgrades array is not properly configured. Creating empty array.");
            upgrades = new UpgradeData[6];
        }

        Debug.Log("[UpgradeManager] Initialized.");
    }

    public UpgradeData GetUpgrade(UpgradeType type)
    {
        return upgrades[(int)type];
    }

    public int GetCurrentLevel(UpgradeType type)
    {
        if (upgrades[(int)type] != null)
        {
            return upgrades[(int)type].currentLevel;
        }
        return 0;
    }

    public bool TryPurchaseUpgrade(UpgradeType type, CurrencyManager currencyManager)
    {
        UpgradeData upgrade = upgrades[(int)type];
        if (upgrade == null)
        {
            Debug.LogWarning($"[UpgradeManager] Upgrade {type} is not assigned.");
            return false;
        }

        if (upgrade.currentLevel >= upgrade.MaxLevel)
        {
            Debug.Log($"[UpgradeManager] {upgrade.upgradeName} is already at max level.");
            return false;
        }

        int cost = upgrade.GetCostForNextLevel();
        if (currencyManager.TrySpendCurrency(cost))
        {
            upgrade.PurchaseLevel();
            Debug.Log($"[UpgradeManager] Purchased {upgrade.upgradeName} level {upgrade.currentLevel}. Cost: {cost}");
            return true;
        }

        Debug.Log($"[UpgradeManager] Not enough currency for {upgrade.upgradeName}. Need: {cost}");
        return false;
    }

    public float GetDamageMultiplier()
    {
        int level = GetCurrentLevel(UpgradeType.Damage);
        return 1f + (level * 0.2f); // 1.0 → 1.2 → 1.4 → 1.6 → 1.8 → 2.0
    }

    public float GetFireRateMultiplier()
    {
        int level = GetCurrentLevel(UpgradeType.FireRate);
        return 1f + (level * 0.15f); // 1.0 → 1.15 → 1.30 → 1.45 → 1.60 → 1.75
    }

    public float GetMoveSpeedMultiplier()
    {
        int level = GetCurrentLevel(UpgradeType.MoveSpeed);
        return 1f + (level * 0.1f); // 1.0 → 1.1 → 1.2 → 1.3 → 1.4 → 1.5
    }

    public float GetExtractionBonusMultiplier()
    {
        int level = GetCurrentLevel(UpgradeType.ExtractionBonus);
        return 1f + (level * 0.1f); // 1.0 → 1.1 → 1.2 → 1.3 → 1.4 → 1.5
    }

    public float GetDashChargeMultiplier()
    {
        int level = GetCurrentLevel(UpgradeType.DashCharge);
        return 1f + (level * 0.2f); // 1.0 → 1.2 → 1.4 → 1.6 → 1.8 → 2.0
    }

    public float GetPickupRadiusMultiplier()
    {
        int level = GetCurrentLevel(UpgradeType.PickupRadius);
        return 1f + (level * 0.1f); // 1.0 → 1.1 → 1.2 → 1.3 → 1.4 → 1.5
    }
}
