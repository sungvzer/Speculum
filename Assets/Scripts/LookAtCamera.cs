using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Transform mainCamera;

    void Start()
    {
        mainCamera = Camera.main.transform;
    }

    void LateUpdate()
    {
        transform.forward = mainCamera.forward;
    }
}
