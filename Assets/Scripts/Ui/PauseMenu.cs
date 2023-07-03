using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused;

    public void PauseGame()
    {
        Time.timeScale = 0f;
        gameIsPaused = true;

    }
    public void Resume()
    {
        Time.timeScale = 1;
        gameIsPaused = true;
    }
    public void Close()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
