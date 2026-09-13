using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider))]
public class MenuObject3D : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Interaction Settings")]
    public UnityEvent onClickAction;

    [Tooltip("The tooltip label to show when hovering over the object.")]
    public GameObject tooltipLabel;

    [Header("Affordance Settings")]
    public float hoverScale = 1.1f;
    public float floatSpeed = 1f;
    public float floatAmplitude = 0.1f;

    private Vector3 originalScale;
    private Vector3 originalPosition;
    private Transform mainCamera;

    void LateUpdate()
    {
        tooltipLabel.transform.forward = mainCamera.forward;
    }

    void Start()
    {
        originalScale = transform.localScale;
        originalPosition = transform.position;

        if (tooltipLabel != null)
        {
            tooltipLabel.SetActive(false);
        }

        if (mainCamera == null) mainCamera = Camera.main.transform;
    }
    void Update()
    {
        // Mathf.sin returns range [-1,1],              + 1f -> [0,2] / 2f -> [0,1]
        float floatOffset = (Mathf.Sin(Time.time * floatSpeed) + 1f) / 2f * floatAmplitude;

        transform.position = new Vector3(originalPosition.x, originalPosition.y + floatOffset, originalPosition.z);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = originalScale * hoverScale;

        if (tooltipLabel != null) tooltipLabel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = originalScale;

        if (tooltipLabel != null) tooltipLabel.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        onClickAction.Invoke();
        transform.localScale = originalScale;

        if (tooltipLabel != null) tooltipLabel.SetActive(false);
    }
}
