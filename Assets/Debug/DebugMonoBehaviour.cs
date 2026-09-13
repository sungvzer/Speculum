using UnityEngine;

public class DebugMonoBehaviour : MonoBehaviour
{
    void Awake()
    {
        CheckReleaseMode();
    }

    void CheckReleaseMode()
    {
#if !UNITY_EDITOR && !DEVELOPMENT_BUILD
        gameObject.SetActive(false);
#endif

    }
}
