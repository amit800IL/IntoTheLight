using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour
{
    private IntoTheLight inputActions;
    public IntoTheLight InputActions { get => inputActions; private set => inputActions = value; }
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

    private void Start()
    {
        InputActions = new IntoTheLight();
        InputActions.Enable();
    }

    public Vector2 GetMoveValue(InputValue input) => input.Get<Vector2>();
    public Vector2 GetMouseDelta(InputValue input) => input.Get<Vector2>();

}




