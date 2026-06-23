using UnityEngine;

public class ExtractionZone : MonoBehaviour
{
    [SerializeField] private float channelDuration = 5f;
    [SerializeField] private Material activeMaterial;
    [SerializeField] private Material inactiveZoneMaterial;
    [SerializeField] private int baseCurrencyReward = 75;
    [SerializeField] private int currencyPerMinute = 15;

    private bool _isActive;
    private bool _playerInZone;
    private float _channelTimer;
    private MeshRenderer _meshRenderer;
    private Material _originalMaterial;

    public delegate void ExtractionCompleteEvent(float runTime, int currencyEarned);
    public delegate void ExtractionEvent();
    public static event ExtractionCompleteEvent OnExtractionComplete;
    public static event ExtractionEvent OnExtractionCancelled;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        if (_meshRenderer != null)
        {
            _originalMaterial = _meshRenderer.material;
        }

        _isActive = false;
        _playerInZone = false;
        _channelTimer = 0f;
        
        SetVisuals(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInZone = true;
            Debug.Log("[ExtractionZone] Player entered zone.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInZone = false;
            _channelTimer = 0f;
            Debug.Log("[ExtractionZone] Player left zone. Channel reset.");
        }
    }

    private void Update()
    {
        if (!_isActive || !_playerInZone)
        {
            return;
        }

        _channelTimer += Time.deltaTime;
        if (_channelTimer >= channelDuration)
        {
            CompleteExtraction();
        }
    }

    public void Activate()
    {
        _isActive = true;
        _channelTimer = 0f;
        _playerInZone = false;
        SetVisuals(true);
        Debug.Log("[ExtractionZone] Activated.");
    }

    public void Deactivate()
    {
        _isActive = false;
        _channelTimer = 0f;
        _playerInZone = false;
        SetVisuals(false);
        Debug.Log("[ExtractionZone] Deactivated.");
    }

    public void CancelExtraction()
    {
        if (_isActive)
        {
            _channelTimer = 0f;
            OnExtractionCancelled?.Invoke();
            Debug.Log("[ExtractionZone] Extraction cancelled.");
        }
    }

    private void CompleteExtraction()
    {
        _isActive = false;
        float runTime = Time.time; // This will be passed from RunManager
        int currencyEarned = CalculateCurrencyReward(runTime);
        OnExtractionComplete?.Invoke(runTime, currencyEarned);
        Debug.Log($"[ExtractionZone] Extraction complete! Earned {currencyEarned} currency.");
    }

    private int CalculateCurrencyReward(float runTime)
    {
        int minutesSurvived = (int)(runTime / 60f);
        return baseCurrencyReward + (minutesSurvived * currencyPerMinute);
    }

    private void SetVisuals(bool active)
    {
        if (_meshRenderer == null)
        {
            return;
        }

        if (active)
        {
            if (activeMaterial != null)
            {
                _meshRenderer.material = activeMaterial;
            }
        }
        else
        {
            if (inactiveZoneMaterial != null)
            {
                _meshRenderer.material = inactiveZoneMaterial;
            }
            else if (_originalMaterial != null)
            {
                _meshRenderer.material = _originalMaterial;
            }
        }
    }

    public float GetChannelProgress()
    {
        return _isActive ? Mathf.Clamp01(_channelTimer / channelDuration) : 0f;
    }

    public bool IsChanneling => _isActive && _playerInZone;
}
