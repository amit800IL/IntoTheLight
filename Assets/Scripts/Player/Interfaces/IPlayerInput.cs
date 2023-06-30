using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

interface IPlayerInput
{
    IEnumerator CheckPlayerInput(Collider other = null, LightGhost ghost = null);
}
