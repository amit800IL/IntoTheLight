using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    private Vector3 newMove;
    private Vector3 moveInput;
    private float blendX, blendY;
    [SerializeField] private float blendSpeed;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float GroundDistance;
    [SerializeField] private Rigidbody playerRigidBody;
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Animator playerAnimator;
   

    private void Update()
    {

        IsGrounded();

    }

    private void OnMove(InputValue value)
    {
        newMove = InputManager.Instance.GetMoveValue(value);

        newMove = new Vector3(-newMove.x, 0, -newMove.y);

        newMove = moveInput.x * transform.right + moveInput.y * transform.forward;

        blendX = Mathf.MoveTowards(blendX, moveInput.x, blendSpeed * Time.deltaTime);
        blendY = Mathf.MoveTowards(blendY, moveInput.y, blendSpeed * Time.deltaTime);

        if (newMove.magnitude > 1)
        {
            newMove.Normalize();
        }


        if (playerRigidBody.velocity == Vector3.zero)
        {
            playerAnimator.SetBool("IsWalking", false);
        }

        else
        {
            playerAnimator.SetBool("IsWalking", true);
        }

        playerAnimator.SetFloat("Horizontal", blendX);
        playerAnimator.SetFloat("Vertical", blendY);


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
