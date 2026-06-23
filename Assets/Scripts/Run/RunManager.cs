using UnityEngine;

public class RunManager : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private EnemySpawner enemySpawner;

    public float RunTime { get; private set; }
    public bool IsRunActive { get; private set; }

    private float _runStartTime;

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

        StartNewRun();
    }

    private void Update()
    {
        if (!IsRunActive)
        {
            return;
        }

        RunTime = Time.time - _runStartTime;

        if (playerHealth != null && playerHealth.IsDead)
        {
            EndRun();
        }
    }

    public void StartNewRun()
    {
        IsRunActive = true;
        _runStartTime = Time.time;
        RunTime = 0f;

        if (playerHealth != null)
        {
            playerHealth.RestoreFullHealth();
        }

        if (enemySpawner != null)
        {
            enemySpawner.Reset();
        }

        Debug.Log("[RunManager] New run started.");
    }

    public void EndRun()
    {
        IsRunActive = false;
        Debug.Log($"[RunManager] Run ended. Survival time: {RunTime:F2} seconds");
    }

    public void RestartRun()
    {
        StartNewRun();
    }
}
