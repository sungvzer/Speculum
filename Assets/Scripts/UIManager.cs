using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public AudioSource click;
    public void OpenScene(
        string sceneName
    )
    {
        click.Play();

        StartCoroutine(Load(sceneName));
    }

    public void ExitGame()
    {
        click.Play();
        Application.Quit();
    }

    IEnumerator Load(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            yield return null;
        }
    }
}
