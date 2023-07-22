using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    private static bool gameIsPaused;
    [SerializeField] private InputActionsSO inputActions;
    [SerializeField] private GameObject menu;

    private void OnEnable()
    {
        inputActions.Pause.performed += PlayerPause;
        inputActions.Pause.canceled += PlayerPause;
    }

    private void OnDisable()
    {
        inputActions.Pause.performed -= PlayerPause;
        inputActions.Pause.canceled -= PlayerPause;
    }

    private void PlayerPause(InputAction.CallbackContext context)
    {
        if (context.performed && gameIsPaused)
        {
            Resume();
        }
        else if (context.performed && !gameIsPaused)
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        FindObjectOfType<AudioSource>().Pause();
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
        gameIsPaused = true;
        menu.SetActive(true);
        Cursor.visible = true;

    }
    public void Resume()
    {
        FindObjectOfType<AudioSource>().UnPause();
        Time.timeScale = 1;
        gameIsPaused = false;
        menu.SetActive(false);
        Cursor.visible = false;
    }
    public void Close()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
