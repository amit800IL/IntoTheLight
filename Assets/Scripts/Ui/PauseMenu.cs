using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

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
        
    }
}
