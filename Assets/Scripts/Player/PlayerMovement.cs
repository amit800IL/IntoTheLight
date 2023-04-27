using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    private Vector3 newMove;
    private Vector3 moveInput;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float GroundDistance;
    [SerializeField] private Rigidbody playerRigidBody;
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Animator playerAnimator;

    private void Update()
    {

        IsGrounded();

        newMove = moveInput.x * transform.right + moveInput.y * transform.forward;

        if (newMove.magnitude > 1)
        {
            newMove.Normalize();
        }
    }

    private void OnMove(InputValue value)
    {
        newMove = InputManager.Instance.GetMoveValue(value);

        newMove = new Vector3(-newMove.x, 0, -newMove.y);

        if (newMove.magnitude > 0)
        {
            playerAnimator.SetBool("IsWalking", true);
            playerAnimator.SetFloat("MovementSpeed", newMove.magnitude);
        }

        else
        {
            playerAnimator.SetBool("IsWalking", false);
        }

        playerRigidBody.velocity = newMove * playerSpeed;

    }

    private void FixedUpdate()
    {
        if (newMove != null)
        {
            playerRigidBody.AddForce(newMove * playerSpeed, ForceMode.Impulse);
        }
    }

    public void IsGrounded() => Physics.CheckSphere(GroundCheck.position, GroundDistance, groundMask);




}
