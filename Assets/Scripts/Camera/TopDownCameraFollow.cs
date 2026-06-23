using UnityEngine;

public class TopDownCameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0f, 16f, -12f);
    [SerializeField] private float smoothTime = 0.08f;

    private Vector3 _velocity;

    private void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref _velocity, smoothTime);

        transform.LookAt(target.position + Vector3.up * 0.5f);
    }

    public void SetTarget(Transform followTarget)
    {
        target = followTarget;
    }
}
