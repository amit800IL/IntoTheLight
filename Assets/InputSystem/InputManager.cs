using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour
{
    private InputActionsSO inputActions;
    public InputActionsSO InputActions { get => inputActions; private set => inputActions = value; }
    public static InputManager Instance { get; private set; }

    [SerializeField] private PlayerLook playerLook;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

     
    }


}




