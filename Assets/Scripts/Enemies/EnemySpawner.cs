using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawning")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnRadius = 12f;
    [SerializeField] private int maxEnemiesAlive = 50;

    [Header("Spawn Rate")]
    [SerializeField] private float baseSpawnRate = 1f;
    [SerializeField] private float intensityIncreaseInterval = 30f;
    [SerializeField] private float spawnRateIncreasePerInterval = 0.5f;

    private float _currentSpawnRate;
    private float _nextSpawnTime;
    private float _nextIntensityIncreaseTime;
    private int _enemiesAlive;

    private void Awake()
    {
        _currentSpawnRate = baseSpawnRate;
        _nextSpawnTime = Time.time + (1f / _currentSpawnRate);
        _nextIntensityIncreaseTime = Time.time + intensityIncreaseInterval;
    }

    private void Update()
    {
        UpdateIntensity();
        UpdateSpawning();
    }

    private void UpdateIntensity()
    {
        if (Time.time >= _nextIntensityIncreaseTime)
        {
            _currentSpawnRate += spawnRateIncreasePerInterval;
            _nextIntensityIncreaseTime = Time.time + intensityIncreaseInterval;

            Debug.Log($"[EnemySpawner] Intensity increase. New spawn rate: {_currentSpawnRate:F2} enemies/sec");
        }
    }

    private void UpdateSpawning()
    {
        if (Time.time < _nextSpawnTime || _enemiesAlive >= maxEnemiesAlive)
        {
            return;
        }

        SpawnEnemy();
        _nextSpawnTime = Time.time + (1f / Mathf.Max(0.01f, _currentSpawnRate));
    }

    private void SpawnEnemy()
    {
        if (enemyPrefab == null)
        {
            return;
        }

        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        Vector3 spawnPosition = new Vector3(
            Mathf.Cos(angle) * spawnRadius,
            0.5f,
            Mathf.Sin(angle) * spawnRadius
        );

        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, transform);
        _enemiesAlive++;

        EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.OnDeath += OnEnemyDeath;
        }
    }

    private void OnEnemyDeath()
    {
        _enemiesAlive = Mathf.Max(0, _enemiesAlive - 1);
    }

    public void Reset()
    {
        _currentSpawnRate = baseSpawnRate;
        _nextSpawnTime = Time.time + (1f / _currentSpawnRate);
        _nextIntensityIncreaseTime = Time.time + intensityIncreaseInterval;

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        _enemiesAlive = 0;
    }
}
