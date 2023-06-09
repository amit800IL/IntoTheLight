using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused;
    public UnityEvent GamePaused;
    public UnityEvent GameResumed;

    private void Start()
    {
        gameIsPaused = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameIsPaused = !gameIsPaused;
            PauseGame();
        }
    }
    void PauseGame()
    {
        if (gameIsPaused)
        {
            Time.timeScale = 0f;
            GamePaused.Invoke();
        }
        else
        {
            Time.timeScale = 1;
            GameResumed.Invoke();
        }
    }
}
