using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgradePanelUI : MonoBehaviour
{
    [SerializeField] private UpgradeManager.UpgradeType upgradeType;
    [SerializeField] private TextMeshProUGUI upgradeName;
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private Button buyButton;
    [SerializeField] private UpgradeScreen parentScreen;

    private UpgradeData _upgradeData;

    private void Start()
    {
        if (parentScreen == null)
        {
            parentScreen = GetComponentInParent<UpgradeScreen>(true);
        }

        _upgradeData = UpgradeManager.Instance.GetUpgrade(upgradeType);

        if (upgradeName != null && _upgradeData != null)
        {
            upgradeName.text = _upgradeData.upgradeName;
        }

        if (description != null && _upgradeData != null)
        {
            description.text = _upgradeData.description;
        }

        if (buyButton != null)
        {
            buyButton.onClick.AddListener(OnBuyClicked);
        }

        RefreshDisplay();
    }

    public void RefreshDisplay()
    {
        if (_upgradeData == null)
        {
            return;
        }

        // Update level display
        if (levelText != null)
        {
            levelText.text = $"Level {_upgradeData.currentLevel}/{_upgradeData.MaxLevel}";
        }

        // Update cost display
        if (costText != null)
        {
            if (_upgradeData.currentLevel >= _upgradeData.MaxLevel)
            {
                costText.text = "MAX";
                if (buyButton != null)
                {
                    buyButton.interactable = false;
                }
            }
            else
            {
                int cost = _upgradeData.GetCostForNextLevel();
                costText.text = $"Cost: {cost}";

                // Enable/disable button based on currency
                if (buyButton != null && CurrencyManager.Instance != null)
                {
                    buyButton.interactable = CurrencyManager.Instance.GetTotalCurrency() >= cost;
                }
            }
        }
    }

    private void OnBuyClicked()
    {
        if (UpgradeManager.Instance != null && CurrencyManager.Instance != null)
        {
            bool success = UpgradeManager.Instance.TryPurchaseUpgrade(upgradeType, CurrencyManager.Instance);
            if (success)
            {
                RefreshDisplay();
                if (parentScreen != null)
                {
                    parentScreen.OnUpgradePurchased();
                }
            }
        }
    }
}
