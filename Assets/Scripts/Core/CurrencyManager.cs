using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance { get; private set; }

    private int _totalCurrency;

    public delegate void CurrencyChangeEvent(int newTotal);
    public static event CurrencyChangeEvent OnCurrencyChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        _totalCurrency = 0;
        Debug.Log("[CurrencyManager] Initialized.");
    }

    public int GetTotalCurrency()
    {
        return _totalCurrency;
    }

    public void AddCurrency(int amount)
    {
        if (amount < 0)
        {
            Debug.LogWarning("[CurrencyManager] Attempted to add negative currency.");
            return;
        }

        _totalCurrency += amount;
        OnCurrencyChanged?.Invoke(_totalCurrency);
        Debug.Log($"[CurrencyManager] Added {amount} currency. Total: {_totalCurrency}");
    }

    public bool TrySpendCurrency(int amount)
    {
        if (amount < 0 || amount > _totalCurrency)
        {
            Debug.LogWarning($"[CurrencyManager] Cannot spend {amount} currency. Total available: {_totalCurrency}");
            return false;
        }

        _totalCurrency -= amount;
        OnCurrencyChanged?.Invoke(_totalCurrency);
        Debug.Log($"[CurrencyManager] Spent {amount} currency. Total: {_totalCurrency}");
        return true;
    }

    public void ResetCurrency()
    {
        _totalCurrency = 0;
        OnCurrencyChanged?.Invoke(_totalCurrency);
        Debug.Log("[CurrencyManager] Currency reset.");
    }
}
