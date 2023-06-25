using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class KeyScript : MonoBehaviour
{
    [HideInInspector] public bool Haskey { get; private set; } = false;

    private bool pickUpAllowed = false;


    private void Update()
    {
        if (pickUpAllowed && Keyboard.current.eKey.isPressed)
        {
            PickUp();
        }
    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            pickUpAllowed = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            pickUpAllowed = false;
        }
    }

    private void PickUp()
    {
        Destroy(gameObject);
        Haskey = true;
    }
}
