using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinGameScript : MonoBehaviour
{
    private AudioSource[] audioSorces;

    private void Start()
    {
        PlayerOpenExitDoor.OnWin += WinPanelActivate;
        gameObject.SetActive(false);
    }
    private void WinPanelActivate()
    {
        gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;

        audioSorces = FindObjectsOfType<AudioSource>();

        foreach (AudioSource source in audioSorces)
        {
            source.Stop();
        }

        Cursor.visible = true;
        Time.timeScale = 0f;
    }
    public void RestartLevel()
    {
        audioSorces = FindObjectsOfType<AudioSource>();

        foreach (AudioSource source in audioSorces)
        {
            source.Play();
        }

        SceneManager.LoadScene("MazeScene");
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
