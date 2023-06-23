using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class FakeKeyScript : MonoBehaviour
{
    [SerializeField]
    private Text pickUpText;

    private bool pickUpAllowed = false;

    [SerializeField] private Guard guard;

    private void Start()
    {
        pickUpText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (pickUpAllowed && Keyboard.current.eKey.isPressed)
        {
            Debug.Log("e pressed");
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
            guard.StartCoroutine(guard.CalculateRoute());
        }
    }

    private void PickUp()
    {
        Debug.Log("pick up pressed");
        Destroy(gameObject);
    }
}
