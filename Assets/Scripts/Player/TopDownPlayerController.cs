using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class TopDownPlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float acceleration = 30f;

    [Header("Aim")]
    [SerializeField] private float rotationSharpness = 20f;
    [SerializeField] private Camera aimCamera;

    private Rigidbody _rb;
    private Vector3 _desiredVelocity;
    private Vector3 _aimPoint;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();

        if (aimCamera == null)
        {
            aimCamera = Camera.main;
        }

        _rb.interpolation = RigidbodyInterpolation.Interpolate;
        _rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    private void Update()
    {
        ReadMovementInput();
        UpdateAimPoint();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        ApplyRotation();
    }

    private void ReadMovementInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 inputDirection = new Vector3(x, 0f, z).normalized;
        
        // Apply move speed upgrade multiplier
        float speedMultiplier = UpgradeManager.Instance != null 
            ? UpgradeManager.Instance.GetMoveSpeedMultiplier() 
            : 1f;
        _desiredVelocity = inputDirection * (moveSpeed * speedMultiplier);
    }

    private void UpdateAimPoint()
    {
        if (aimCamera == null)
        {
            return;
        }

        Ray mouseRay = aimCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, new Vector3(0f, transform.position.y, 0f));

        if (groundPlane.Raycast(mouseRay, out float enter))
        {
            _aimPoint = mouseRay.GetPoint(enter);
        }
    }

    private void ApplyMovement()
    {
        Vector3 currentVelocity = _rb.velocity;
        Vector3 planarVelocity = new Vector3(currentVelocity.x, 0f, currentVelocity.z);

        Vector3 newPlanarVelocity = Vector3.MoveTowards(
            planarVelocity,
            _desiredVelocity,
            acceleration * Time.fixedDeltaTime
        );

        _rb.velocity = new Vector3(newPlanarVelocity.x, currentVelocity.y, newPlanarVelocity.z);
    }

    private void ApplyRotation()
    {
        Vector3 lookDirection = _aimPoint - transform.position;
        lookDirection.y = 0f;

        if (lookDirection.sqrMagnitude < 0.0001f)
        {
            return;
        }

        Quaternion targetRotation = Quaternion.LookRotation(lookDirection.normalized, Vector3.up);
        Quaternion smoothedRotation = Quaternion.Slerp(
            _rb.rotation,
            targetRotation,
            rotationSharpness * Time.fixedDeltaTime
        );

        _rb.MoveRotation(smoothedRotation);
    }
}
