using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class KeyScript : MonoBehaviour
{
    [SerializeField]
    private Text pickUpText;

    private bool pickUpAllowed = false;

    // Use this for initialization
    private void Start()
    {
        pickUpText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        if (pickUpAllowed && Input.GetKeyDown(KeyCode.K))
            PickUp();
    }

    
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name.Equals("NewPlayer"))
        {
            pickUpText.gameObject.SetActive(true);
            pickUpAllowed = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.name.Equals("NewPlayer"))
        {
            pickUpText.gameObject.SetActive(false);
            pickUpAllowed = false;
        }
    }

    private void PickUp()
    {
        Destroy(this);
    }
}
