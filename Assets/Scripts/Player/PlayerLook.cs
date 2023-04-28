using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UIElements;

public class PlayerLook : MonoBehaviour
{
    private Vector2 newLook;
    [SerializeField] float LookSpeed;
    [SerializeField] Transform orientation;
    public void OnLook(InputValue value)
    {
        newLook = InputManager.Instance.GetLookValue(value);

        transform.Rotate(0, newLook.x * LookSpeed * Time.deltaTime, 0);
    }

}

