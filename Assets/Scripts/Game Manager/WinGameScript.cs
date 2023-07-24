using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinGameScript : MonoBehaviour
{

    private AudioSource[] audioSources;

    void Start()
    {
        PlayerOpenExitDoor.OnWin += WinPanelActivate;
        gameObject.SetActive(false);
    }
    private void WinPanelActivate()
    {
        gameObject.SetActive(true);

        audioSources = FindObjectsOfType<AudioSource>();

        foreach (AudioSource source in audioSources)
        {
            source.Stop();
        }
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Quit()
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
