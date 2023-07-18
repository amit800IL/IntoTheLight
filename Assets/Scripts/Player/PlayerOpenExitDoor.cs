using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerOpenExitDoor : MonoBehaviour, ICheckPlayerInteraction
{
    private bool isInDoorTrigger;
    [SerializeField] private RealKey keyScript;
    [SerializeField] private Transform Door;
    private InputAction.CallbackContext context;

    private void Start()
    {
        context = new InputAction.CallbackContext();
    }

    public void OnPlayerInteraction(InputAction.CallbackContext context)
    {
        if (context.performed && keyScript.Haskey && isInDoorTrigger)
        {
            Door.transform.Translate(0, 10000, 0);
            Application.Quit();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("OpenDoor"))
        {
            Debug.Log("The player can open the door, he has the key " + keyScript.Haskey);
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
