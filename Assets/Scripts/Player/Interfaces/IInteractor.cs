using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

interface IInteractor
{
    public void OnInteraction(InputAction.CallbackContext context);
}
