using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerOpenDoor : MonoBehaviour, Iinteraction, IInput
{
    private bool isInDoorTrigger;
    [SerializeField] private KeyScript keyScript;
    [SerializeField] private Transform Door;

    public void OnInteraction(InputAction.CallbackContext context)
    {
        if (context.performed && keyScript.Haskey && isInDoorTrigger)
        {
            Door.transform.Translate(0, 10000, 0);
            Application.Quit();
        }

        else if (context.performed && !keyScript.Haskey && isInDoorTrigger)
        {
            return;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("OpenDoor"))
        {
            isInDoorTrigger = true;
            StartCoroutine(CheckPlayerInput(other));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("OpenDoor"))
        {
            isInDoorTrigger = false;
        }
    }

    public IEnumerator CheckPlayerInput(Collider other)
    {
        OnInteraction(new());

        yield return new WaitForSeconds(1);
    }

}
