using TMPro;
using UnityEngine;

public class VersionWriter : DebugMonoBehaviour
{
    public TextMeshPro timestampLabel;

    public void Start()
    {
        string buildTime = System.DateTime.Now.ToString("dd/MM HH:mm:ss");
        string message = $"Build Time: {buildTime}";

        Debug.Log($"[BUILD VER] {message}");

        if (timestampLabel != null)
        {
            timestampLabel.text = message;
        }
    }

}
