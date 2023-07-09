using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputActions", menuName = "ScriptableObjects/InputActions")]
public class InputActionsSO : ScriptableObject
{
    public InputAction Move;
    public InputAction Look;
    public InputAction Interaction;
    public InputAction Pause;

    private void OnEnable()
    {
        Move.Enable();
        Look.Enable();
        Interaction.Enable();
        Pause.Enable();

    }

    private void OnDisable()
    {
        Move.Disable();
        Look.Disable();
        Interaction.Disable();
        Pause.Disable();
    }
}
