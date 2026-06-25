using UnityEngine;
using TMPro;

public class RunSummaryScreen : MonoBehaviour
{
    [SerializeField] private GameObject screenRoot;
    [SerializeField] private TextMeshProUGUI survivalTimeText;
    [SerializeField] private TextMeshProUGUI runCurrencyText;
    [SerializeField] private TextMeshProUGUI totalCurrencyText;
    [SerializeField] private TextMeshProUGUI continuePromptText;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private UpgradeScreen upgradeScreen;

    private bool _isActive;

    private void Awake()
    {
        EnsureReferences();
        HideAtStartup();
    }

    private void Update()
    {
        if (_isActive && Input.GetKeyDown(KeyCode.Space))
        {
            Hide();
        }
    }

    public void Show(float survivalTime, int currencyEarned, int totalCurrency)
    {
        EnsureReferences();
        _isActive = true;
        
        int minutes = (int)(survivalTime / 60f);
        int seconds = (int)(survivalTime % 60f);
        
        if (survivalTimeText != null)
        {
            survivalTimeText.text = $"Survival Time: {minutes}:{seconds:D2}";
        }

        if (runCurrencyText != null)
        {
            runCurrencyText.text = $"Run Reward: +{currencyEarned}";
        }

        if (totalCurrencyText != null)
        {
            totalCurrencyText.text = $"Total Currency: {totalCurrency}";
        }

        if (continuePromptText != null)
        {
            continuePromptText.text = "Press SPACE to continue";
        }

        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }

        GetScreenRoot().SetActive(true);
        Time.timeScale = 0f; // Pause the game
    }

    public void Hide()
    {
        EnsureReferences();
        _isActive = false;

        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

        GetScreenRoot().SetActive(false);
        
        // Show upgrade screen instead of resuming game
        if (upgradeScreen != null)
        {
            upgradeScreen.Show();
        }
        else
        {
            Time.timeScale = 1f; // Resume the game if no upgrade screen
        }
    }

    private void EnsureReferences()
    {
        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        if (upgradeScreen == null)
        {
            upgradeScreen = FindObjectOfType<UpgradeScreen>(true);
        }
    }

    private void HideAtStartup()
    {
        _isActive = false;

        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

        GetScreenRoot().SetActive(false);
    }

    private GameObject GetScreenRoot()
    {
        if (screenRoot != null)
        {
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
}
