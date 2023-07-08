using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputActions", menuName = "ScriptableObjects/InputActions")]
public class InputActionsSO : ScriptableObject
{
    public InputAction Move;
    public InputAction Look;
    public InputAction Interaction;


    public void Enable()
    {
        Move.Enable();
        Look.Enable();
        Interaction.Enable();
    }

    public void Disable()
    {
        Move.Disable();
        Look.Disable();
        Interaction.Disable();
    }
}
