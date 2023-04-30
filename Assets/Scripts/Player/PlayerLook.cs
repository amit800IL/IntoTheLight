using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    private Vector2 newLook;
    [SerializeField] float LookSpeed;
    [SerializeField] Transform orientation;

    public void OnLook(InputValue value)
    {
        newLook = InputManager.Instance.GetLookValue(value);

        transform.Rotate(0, newLook.x * LookSpeed * Time.deltaTime, 0);

        if (newLook.magnitude > 1)
        {
            newLook.Normalize();
        }
    }

}

