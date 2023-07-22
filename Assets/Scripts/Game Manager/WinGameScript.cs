using UnityEngine;
using UnityEngine.SceneManagement;

public class WinGameScript : MonoBehaviour
{
    private void Start()
    {
        PlayerOpenExitDoor.OnWin += WinPanelActivate;
        gameObject.SetActive(false);
    }
    private void WinPanelActivate()
    {
        gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        FindObjectOfType<AudioSource>().Stop();
        Time.timeScale = 0f;
    }
    public void RestartLevel()
    {
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
