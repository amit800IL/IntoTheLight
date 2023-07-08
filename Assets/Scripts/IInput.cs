using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

interface IInput
{
    public void OnInteraction(InputAction.CallbackContext context);
}
