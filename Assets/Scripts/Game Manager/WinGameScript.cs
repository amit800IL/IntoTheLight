using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinGameScript : MonoBehaviour
{
    private AudioSource[] audioSorces;

    private void Start()
    {
        gameObject.SetActive(false);
        PlayerOpenExitDoor.OnWin += WinPanelActivate;
    }
    private void OnEnable()
    {
        gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        PlayerOpenExitDoor.OnWin -= WinPanelActivate;
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        audioSorces = FindObjectsOfType<AudioSource>();

        foreach (AudioSource source in audioSorces)
        {
            source.Play();
        }
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
