using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    private Vector3 newLook;
    private Vector2 smoothLook;
    private Vector3 currentRotation;
    private Vector2 smoothMouseDelta;
    //private float xRotation = 0f;
    [SerializeField] private float smoothing = 5f;
    [SerializeField] public float mouseSenstivity = 3.5f;
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private InputActionsSO InputActions;


    private void Start()
    {
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

        smoothMouseDelta = Vector2.Lerp(smoothMouseDelta, newLook, mouseSenstivity * Time.deltaTime);
        currentRotation.x = Mathf.Clamp(currentRotation.x, -90f, 90f);
        currentRotation += new Vector3(-smoothMouseDelta.y, smoothMouseDelta.x * smoothing, 0);
        cameraTransform.localRotation = Quaternion.Euler(currentRotation);

        //float mouseX = newLook.x * mouseSenstivity * Time.deltaTime;
        //float mouseY = newLook.y * mouseSenstivity * Time.deltaTime;

        //xRotation -= mouseY;
        //xRotation = Mathf.Clamp(xRotation, -90f, 90);
        //cameraTransform.localRotation = quaternion.Euler(xRotation, 0, 0);

        //playerTransform.Rotate(Vector3.up * mouseX);
    }

}

