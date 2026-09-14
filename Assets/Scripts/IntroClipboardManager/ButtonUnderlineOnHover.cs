using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonUnderlineOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TMP_Text buttonText;
    private FontStyles originalStyle;

    private void Awake()
    {
        // Recupera il componente TextMeshPro figlio del pulsante
        buttonText = GetComponentInChildren<TMP_Text>();

        if (buttonText != null)
        {
            originalStyle = buttonText.fontStyle;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (buttonText == null) return;

        buttonText.fontStyle |= FontStyles.Underline;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonText == null) return;

        buttonText.fontStyle = originalStyle;
    }

    private void OnDisable()
    {
        if (buttonText != null)
        {
            buttonText.fontStyle = originalStyle;
        }
    }
}
