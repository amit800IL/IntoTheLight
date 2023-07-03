using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused;
    private bool keyPress;
    public GameObject OnPause;
    private void Update()
    {
        keyPress = Keyboard.current.escapeKey.isPressed;
        PauseGame();
    }
    public void PauseGame()
    {
        if (keyPress)
        {
            Time.timeScale = 0f;
            gameIsPaused = true;
            OnPause.SetActive(true);
        }

    }
    public void Resume()
    {
        Time.timeScale = 1;
        gameIsPaused = false;
        OnPause.SetActive(false);
    }
    public void Close()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
