
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    private Vector2 newLook;
    private quaternion initialRotation = quaternion.identity;
    private float xRotation = 0f;
    [SerializeField] public float mouseSenstivity = 3.5f;
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private InputActionsSO InputActions;


    private void Start()
    {
        initialRotation = playerTransform.rotation;
        playerTransform.rotation = initialRotation;
        cameraTransform.localRotation = Quaternion.Euler(Vector3.zero);
        InputActions.Look.Enable();
    }
    private void OnEnable()
    {
        InputActions.Look.performed += OnLook;
        InputActions.Look.canceled += OnLook;
    }

    private void OnDisable()
    {
        InputActions.Look.performed -= OnLook;
        InputActions.Look.canceled -= OnLook;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        newLook = context.ReadValue<Vector2>();

        float mouseX = newLook.x * mouseSenstivity;
        float mouseY = newLook.y * mouseSenstivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90);
        cameraTransform.localRotation = quaternion.Euler(xRotation, 0, 0);

        playerTransform.Rotate(Vector3.up * mouseX);
    }

}

