using UnityEngine;
using TMPro;

public class RunSummaryScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI survivalTimeText;
    [SerializeField] private TextMeshProUGUI runCurrencyText;
    [SerializeField] private TextMeshProUGUI totalCurrencyText;
    [SerializeField] private TextMeshProUGUI continuePromptText;
    [SerializeField] private CanvasGroup canvasGroup;

    private bool _isActive;

    private void Start()
    {
        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        Hide();
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

        gameObject.SetActive(true);
        Time.timeScale = 0f; // Pause the game
        Debug.Log("[RunSummaryScreen] Shown.");
    }

    public void Hide()
    {
        _isActive = false;

        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

        gameObject.SetActive(false);
        Time.timeScale = 1f; // Resume the game
        Debug.Log("[RunSummaryScreen] Hidden.");
    }
}
