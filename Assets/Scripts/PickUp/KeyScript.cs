using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class KeyScript : MonoBehaviour
{
    [HideInInspector] public bool Haskey { get; private set; } = false;

    [SerializeField] private Text pickUpText;

    private bool pickUpAllowed = false;

    private void Start()
    {
        pickUpText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (pickUpAllowed && Keyboard.current.kKey.isPressed)
        {
            Debug.Log("K pressed");
            PickUp();
        }
    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            pickUpText.gameObject.SetActive(true);
            pickUpAllowed = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            pickUpText.gameObject.SetActive(false);
            pickUpAllowed = false;
        }
    }

    private void PickUp()
    {
        Debug.Log("pick up pressed");
        Destroy(gameObject);
        Haskey = true;
    }
}
