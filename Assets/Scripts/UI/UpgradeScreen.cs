using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgradeScreen : MonoBehaviour
{
    [SerializeField] private GameObject screenRoot;
    [SerializeField] private Transform upgradeGridContainer; // Parent for upgrade panels
    [SerializeField] private TextMeshProUGUI totalCurrencyText;
    [SerializeField] private Button exitButton;
    [SerializeField] private CanvasGroup canvasGroup;

    private UpgradePanelUI[] _upgradePanels;
    private bool _isActive;
    private bool _isInitialized;

    private void Awake()
    {
        InitializeIfNeeded();
        SetHiddenVisualState();
    }

    private void InitializeIfNeeded()
    {
        if (_isInitialized)
        {
            return;
        }

        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = GetComponentInParent<CanvasGroup>();
            }
        }

        if (upgradeGridContainer != null)
        {
            _upgradePanels = upgradeGridContainer.GetComponentsInChildren<UpgradePanelUI>(true);
        }
        else
        {
            _upgradePanels = GetComponentsInChildren<UpgradePanelUI>(true);
        }

        if (exitButton != null)
        {
            exitButton.onClick.RemoveListener(Hide);
            exitButton.onClick.AddListener(Hide);
        }

        _isInitialized = true;
    }

    private void Update()
    {
        if (_isActive && Input.GetKeyDown(KeyCode.Escape))
        {
            Hide();
        }
    }

    public void Show()
    {
        InitializeIfNeeded();
        GameObject root = GetScreenRoot();
        root.SetActive(true);
        _isActive = true;

        // Refresh all panels
        for (int i = 0; i < _upgradePanels.Length; i++)
        {
            _upgradePanels[i].RefreshDisplay();
        }

        UpdateCurrencyDisplay();

        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
    }

    public void Hide()
    {
        _isActive = false;
        SetHiddenVisualState();

        GameObject root = GetScreenRoot();
        root.SetActive(false);
        Time.timeScale = 1f; // Resume the game
    }

    private GameObject GetScreenRoot()
    {
        if (screenRoot != null)
        {
            return screenRoot;
        }

        Canvas parentCanvas = GetComponentInParent<Canvas>();
        if (parentCanvas != null)
        {
            screenRoot = parentCanvas.gameObject;
            return screenRoot;
        }

        if (canvasGroup != null)
        {
            screenRoot = canvasGroup.gameObject;
        }
        else
        {
            screenRoot = gameObject;
        }

        return screenRoot;
    }

    private void SetHiddenVisualState()
    {
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }

    public void UpdateCurrencyDisplay()
    {
        if (totalCurrencyText != null && CurrencyManager.Instance != null)
        {
            totalCurrencyText.text = $"Total Currency: {CurrencyManager.Instance.GetTotalCurrency()}";
        }
    }

    public void OnUpgradePurchased()
    {
        UpdateCurrencyDisplay();
        
        // Refresh all panels to reflect new states
        for (int i = 0; i < _upgradePanels.Length; i++)
        {
            _upgradePanels[i].RefreshDisplay();
        }
    }
}
