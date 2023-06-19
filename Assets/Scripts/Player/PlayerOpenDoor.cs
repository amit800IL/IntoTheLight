using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerOpenDoor : MonoBehaviour
{
    private bool keyPress;
    private bool isInDoorTrigger;
    [SerializeField] private KeyScript keyScript;
    [SerializeField] private Transform Door;


    private void Update()
    {
        keyPress = Keyboard.current.fKey.isPressed;
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

    private IEnumerator CheckPlayerInput(Collider other)
    {
        while (isInDoorTrigger)
        {
            if (keyPress && keyScript.Haskey)
            {
                Door.transform.Translate(0, 10000, 0);
                Application.Quit();
            }

            else if (keyPress && !keyScript.Haskey)
            {
                break;
            }

            yield return null;
        }
    }
}
