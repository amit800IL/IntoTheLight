using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    private Vector3 newLook;
    [SerializeField] private Transform orientation;
    [SerializeField] private InputActionsSO InputActions;
    [SerializeField] private PlayerStatsSO playerStats;

    private void OnEnable()
    {
        InputActions.Enable();
        InputActions.Look.performed += OnLook;
    }

    private void OnDisable()
    {
        InputActions.Disable();
        InputActions.Look.performed -= OnLook;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        newLook = context.ReadValue<Vector2>();

        transform.Rotate(0, newLook.x * playerStats.lookSpeed * Time.deltaTime, 0);
    }

}