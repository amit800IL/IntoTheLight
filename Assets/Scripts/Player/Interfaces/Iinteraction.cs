using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

interface Iinteraction
{
    IEnumerator CheckPlayerInput(Collider other);
}
