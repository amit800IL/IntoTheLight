using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerOpenExitDoor : MonoBehaviour, ICheckPlayerInteraction
{
    private bool isInDoorTrigger;
    [SerializeField] private RealKey keyScript;
    [SerializeField] private Transform Door;
    [SerializeField] private InputActionsSO inputActions;
    private InputAction.CallbackContext context;
    public delegate void WinGameAction();
    public static event WinGameAction OnWin;
    private void Start()
    {
        context = new InputAction.CallbackContext();
    }

    private void OnEnable()
    {
        inputActions.Interaction.performed += OnPlayerInteraction;
        inputActions.Interaction.canceled += OnPlayerInteraction;
    }

    private void OnDisable()
    {
        inputActions.Interaction.performed -= OnPlayerInteraction;
        inputActions.Interaction.canceled -= OnPlayerInteraction;
    }

    public void OnPlayerInteraction(InputAction.CallbackContext context)
    {
        if (context.performed && keyScript.Haskey && isInDoorTrigger)
        {
            Door.transform.Translate(0, 10000, 0);
            if (OnWin != null)
            {
                OnWin();
                Debug.Log("on win invoked");
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("OpenDoor"))
        {
            isInDoorTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("OpenDoor"))
        {
            isInDoorTrigger = false;
        }
    }

}
