using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour, IAnimationBlender
{
    private Vector3 newLook;
    private float blendX;
    private float blendSpeed = 1f;
    [SerializeField] private float LookSpeed;
    [SerializeField] private Transform orientation;
    [SerializeField] private float AnimationAccelrator;
    [SerializeField] private PlayerMovement playerMovement;

    private void Update()
    {
        AnimationBlend();
    }

    public void OnLook(InputValue value)
    {
        newLook = InputManager.Instance.GetMouseDelta(value);

        transform.Rotate(0, newLook.x * LookSpeed * Time.deltaTime, 0);

    }

    public void AnimationBlend()
    {
        playerMovement.playerAnimator.SetBool("IsWalking", true);

        blendX = Mathf.MoveTowards(blendX, newLook.x, blendSpeed * Time.deltaTime * AnimationAccelrator);

        playerMovement.playerAnimator.SetFloat("Horizontal", blendX);

    }

}