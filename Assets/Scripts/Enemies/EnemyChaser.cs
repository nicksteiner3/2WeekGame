using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyChaser : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3.75f;
    [SerializeField] private float acceleration = 20f;
    [SerializeField] private float rotationSharpness = 12f;
    [SerializeField] private Transform target;

    private Rigidbody _rb;
    private Vector3 _desiredVelocity;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.interpolation = RigidbodyInterpolation.Interpolate;
        _rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    private void Start()
    {
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                target = player.transform;
            }
        }
    }

    private void Update()
    {
        if (target == null)
        {
            _desiredVelocity = Vector3.zero;
            return;
        }

        Vector3 toTarget = target.position - transform.position;
        toTarget.y = 0f;

        if (toTarget.sqrMagnitude < 0.01f)
        {
            _desiredVelocity = Vector3.zero;
            return;
        }

        _desiredVelocity = toTarget.normalized * moveSpeed;
    }

    private void FixedUpdate()
    {
        Vector3 currentVelocity = _rb.velocity;
        Vector3 planarVelocity = new Vector3(currentVelocity.x, 0f, currentVelocity.z);

        Vector3 newPlanarVelocity = Vector3.MoveTowards(
            planarVelocity,
            _desiredVelocity,
            acceleration * Time.fixedDeltaTime
        );

        _rb.velocity = new Vector3(newPlanarVelocity.x, currentVelocity.y, newPlanarVelocity.z);

        Vector3 facing = new Vector3(newPlanarVelocity.x, 0f, newPlanarVelocity.z);
        if (facing.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(facing.normalized, Vector3.up);
            Quaternion smoothedRotation = Quaternion.Slerp(
                _rb.rotation,
                targetRotation,
                rotationSharpness * Time.fixedDeltaTime
            );
            _rb.MoveRotation(smoothedRotation);
        }
    }
}
