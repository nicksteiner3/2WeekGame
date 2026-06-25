using UnityEngine;

public class RunManager : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private ExtractionZone extractionZone;
    [SerializeField] private RunSummaryScreen runSummaryScreen;
    [SerializeField] private float extractionSpawnTime = 3f; // 3 seconds for testing (change to 180 for 3 minutes)

    public float RunTime { get; private set; }
    public bool IsRunActive { get; private set; }
    public bool ExtractionActive { get; private set; }

    private float _runStartTime;
    private bool _extractionSpawned;

    private void Start()
    {
        if (playerHealth == null)
        {
            playerHealth = FindObjectOfType<PlayerHealth>();
        }

        if (enemySpawner == null)
        {
            enemySpawner = FindObjectOfType<EnemySpawner>();
        }

        if (extractionZone == null)
        {
            extractionZone = FindObjectOfType<ExtractionZone>();
        }

        if (runSummaryScreen == null)
        {
            runSummaryScreen = FindObjectOfType<RunSummaryScreen>(true);
        }

        // Subscribe to extraction events
        if (extractionZone != null)
        {
            ExtractionZone.OnExtractionComplete += HandleExtractionComplete;
        }

        // Subscribe to player death
        if (playerHealth != null)
        {
            playerHealth.OnDeath += HandlePlayerDeath;
        }

        StartNewRun();
    }

    private void OnDestroy()
    {
        if (extractionZone != null)
        {
            ExtractionZone.OnExtractionComplete -= HandleExtractionComplete;
        }

        if (playerHealth != null)
        {
            playerHealth.OnDeath -= HandlePlayerDeath;
        }
    }

    private void Update()
    {
        if (!IsRunActive)
        {
            return;
        }

        RunTime = Time.time - _runStartTime;

        // Spawn extraction zone at 3:00
        if (!_extractionSpawned && RunTime >= extractionSpawnTime && extractionZone != null)
        {
            _extractionSpawned = true;
            ExtractionActive = true;
            extractionZone.Activate();
            Debug.Log("[RunManager] Extraction zone activated!");
        }

        if (playerHealth != null && playerHealth.IsDead)
        {
            EndRun();
        }
    }

    public void StartNewRun()
    {
        IsRunActive = true;
        ExtractionActive = false;
        _runStartTime = Time.time;
        RunTime = 0f;
        _extractionSpawned = false;

        if (playerHealth != null)
        {
            playerHealth.RestoreFullHealth();
        }

        if (enemySpawner != null)
        {
            enemySpawner.Reset();
        }

        if (extractionZone != null)
        {
            extractionZone.Deactivate();
        }

        Debug.Log("[RunManager] New run started.");
    }

    public void EndRun()
    {
        IsRunActive = false;
        if (extractionZone != null)
        {
            extractionZone.Deactivate();
        }
        Debug.Log($"[RunManager] Run ended. Survival time: {RunTime:F2} seconds");
    }

    public void RestartRun()
    {
        StartNewRun();
    }

    private void HandleExtractionComplete(float runTime, int currencyEarned)
    {
        ExtractionActive = false;
        
        // Award currency
        if (CurrencyManager.Instance != null)
        {
            CurrencyManager.Instance.AddCurrency(currencyEarned);
        }

        // Show summary screen
        if (runSummaryScreen != null)
        {
            int totalCurrency = CurrencyManager.Instance != null 
                ? CurrencyManager.Instance.GetTotalCurrency() 
                : 0;
            runSummaryScreen.Show(runTime, currencyEarned, totalCurrency);
        }

        Debug.Log("[RunManager] Extraction completed! Run successful.");
        EndRun();
    }

    private void HandlePlayerDeath()
    {
        if (ExtractionActive && extractionZone != null)
        {
            extractionZone.CancelExtraction();
            Debug.Log("[RunManager] Player died during extraction. Extraction cancelled.");
        }
    }
}


