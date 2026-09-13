using UnityEngine;

public class UIManager : MonoBehaviour
{
    public AudioSource click;
    public void OpenSettings()
    {
        click.Play();
    }

    public void ExitGame()
    {
        click.Play();
        Application.Quit();
    }
}
