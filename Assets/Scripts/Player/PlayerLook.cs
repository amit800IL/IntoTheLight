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
    private float xRotation = 0f;
    public void OnLook(InputValue value)
    {
        newLook = InputManager.Instance.GetLookValue(value);

        float xRot = -newLook.y * LookSpeed * Time.deltaTime;
        xRotation += xRot;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.Rotate(0, newLook.x * LookSpeed * Time.deltaTime, 0);
    }

}

