using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

interface ICheckPlayerInteraction
{
    public void OnPlayerInteraction(InputAction.CallbackContext context);
}
