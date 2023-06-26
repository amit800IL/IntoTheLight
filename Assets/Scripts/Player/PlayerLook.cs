using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    private Vector2 newLook;
    [SerializeField] private float LookSpeed;
    [SerializeField] private Transform orientation;
    [SerializeField] private float AnimationAccelrator;
    private float blendX;
    private float blendSpeed = 1f;



    public void OnLook(InputValue value)
    {
        newLook = InputManager.Instance.GetMouseDelta(value);

        //if (newLook.magnitude > 1)
        //{
        //    newLook.Normalize();
        //}

        AnimationBlend();

        transform.Rotate(0, newLook.x * LookSpeed * Time.deltaTime, 0);

    }

    public void AnimationBlend()
    {
        blendX = Mathf.MoveTowards(blendX, newLook.x, blendSpeed * Time.deltaTime * AnimationAccelrator);

        GameManager.Instance.PlayerMovement.playerAnimator.SetFloat("Horizontal", blendX);
    }

}