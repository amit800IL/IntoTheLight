using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    private Vector3 newMove;
    private float blendX, blendY;
    private bool isMovingBackwards;
    [SerializeField] private float AnimationAccelrator;
    [SerializeField] private float blendSpeed;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float GroundDistance;
    [SerializeField] private Rigidbody playerRigidBody;
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private AudioSource playerWalk;


    private void Update()
    {
        IsGrounded();

        AnimationBlend();

        Evasion();

        Running();

    }

    private void AnimationBlend()
    {
        blendX = Mathf.MoveTowards(blendX, -newMove.x, blendSpeed * Time.deltaTime * AnimationAccelrator);
        blendY = Mathf.MoveTowards(blendY, newMove.y, blendSpeed * Time.deltaTime * AnimationAccelrator);

        playerAnimator.SetFloat("Horizontal", blendX);
        playerAnimator.SetFloat("Vertical", blendY);
    }

    public void Evasion()
    {
        if (Keyboard.current.eKey.isPressed)
        {
            playerAnimator.SetBool("IsEvading", true);
        }
        else
        {
            playerAnimator.SetBool("IsEvading", false);
        }
    }

    public void Running()
    {


        if (Keyboard.current.shiftKey.isPressed)
        {
            playerAnimator.SetBool("IsRunning", true);
            playerWalk.pitch = 2f;
        }
        else
        {
            playerAnimator.SetBool("IsRunning", false);
            playerWalk.pitch = 1f;
        }
    }

    private void OnMove(InputValue value)
    {
        newMove = InputManager.Instance.GetMoveValue(value);

        playerWalk.Play();
        playerWalk.volume = 0.5f;

        Vector3 Forward = Camera.main.transform.forward;
        Forward.y = 0f;
        Forward.Normalize();

        Vector3 Right = Camera.main.transform.right;
        Right.y = 0f;
        Right.Normalize();

        Vector3 moveDirection = newMove.x * Right + newMove.y * Forward;
        moveDirection.y = 0f;

        if (newMove.magnitude < 0.1f)
        {
            playerWalk.Stop();
            playerAnimator.SetBool("IsWalking", false);
        }

        if (playerRigidBody.velocity == Vector3.zero)
        {
            playerAnimator.SetBool("IsWalking", false);
        }

        else
        {

            playerAnimator.SetBool("IsWalking", true);

        }


        if (newMove.magnitude > 1)
        {
            newMove.Normalize();
        }
        playerRigidBody.velocity = moveDirection * playerSpeed;

    }



    public void IsGrounded() => Physics.CheckSphere(GroundCheck.position, GroundDistance, groundMask);




}
