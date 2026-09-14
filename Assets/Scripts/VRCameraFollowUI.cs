using UnityEngine;

public class VRCameraFollowUI : MonoBehaviour
{
    public float distanceFromCamera = 1.5f;

    public float followSpeed = 3.0f;

    public float angleDeadzone = 10.0f;

    private Transform mainCamera;

    private void Start()
    {
        if (Camera.main != null)
        {
            mainCamera = Camera.main.transform;
        }

        if (mainCamera != null)
        {
            transform.position = mainCamera.position + (mainCamera.forward * distanceFromCamera);

            transform.rotation = Quaternion.LookRotation(mainCamera.position - transform.position);
        }
    }

    private void LateUpdate()
    {
        if (mainCamera == null) return;

        Vector3 targetPosition = mainCamera.position + (mainCamera.forward * distanceFromCamera);

        float angle = Vector3.Angle(mainCamera.forward, transform.position - mainCamera.position);

        if (angle > angleDeadzone)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followSpeed);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * (followSpeed * 0.5f));
        }

        Quaternion targetRotation = Quaternion.LookRotation(mainCamera.position - transform.position);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * followSpeed);
    }
}
