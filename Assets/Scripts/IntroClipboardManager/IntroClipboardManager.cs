using TMPro;
using UnityEngine;

public class IntroClipboardManager : MonoBehaviour
{
    private int pageIndex = 0;
    public GameObject textDisplay;
    public GameObject nextButton;
    public GameObject prevButton;
    public TextAsset[] textAssets;

    private TMP_Text textMeshPro;

    private void Awake()
    {
        if (textDisplay != null)
        {
            textMeshPro = textDisplay.GetComponent<TMP_Text>();
        }
    }

    private void Start()
    {
        UpdateUI();
    }

    public void NextPage()
    {
        if (textAssets == null || textAssets.Length == 0) return;

        if (pageIndex < textAssets.Length - 1)
        {
            pageIndex++;
            UpdateUI();
        }
    }

    public void PreviousPage()
    {
        if (pageIndex > 0)
        {
            pageIndex--;
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        if (textAssets == null || textAssets.Length == 0) return;

        if (textMeshPro != null && pageIndex >= 0 && pageIndex < textAssets.Length && textAssets[pageIndex] != null)
        {
            textMeshPro.text = textAssets[pageIndex].text;
        }

        if (prevButton != null)
            prevButton.SetActive(pageIndex > 0);

        if (nextButton != null)
            nextButton.SetActive(pageIndex < textAssets.Length - 1);
    }
}
