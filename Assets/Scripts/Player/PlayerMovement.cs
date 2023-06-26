using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour, IAnimationBlender
{

    [field: Header("General")]

    private Vector3 newMove;
    [field: SerializeField] public Animator playerAnimator { get; private set; }
    [field: SerializeField] public Collider playerCollider { get; private set; }

    [SerializeField] private Guard guard;

    [SerializeField] private Rigidbody playerRigidBody;

    [Header("Numbers")]

    [SerializeField] private float AnimationAccelrator;
    [SerializeField] private float playerSpeed;
    private float blendX, blendY;
    private float blendSpeed = 1;

    [Header("Ground Check")]

    [SerializeField] private float GroundDistance;
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private LayerMask groundMask;

    [Header("Audio")]

    [SerializeField] private PlayerVoice playerVoice;

    private void Update()
    {
        IsGrounded();

        AnimationBlend();

        Running();
    }

    public void AnimationBlend()
    {
        blendX = Mathf.MoveTowards(blendX, -newMove.x, blendSpeed * Time.deltaTime * AnimationAccelrator);
        blendY = Mathf.MoveTowards(blendY, newMove.y, blendSpeed * Time.deltaTime * AnimationAccelrator);

        playerAnimator.SetFloat("Horizontal", blendX);
        playerAnimator.SetFloat("Vertical", blendY);
    }

    public void Running()
    {

        if (Keyboard.current.shiftKey.isPressed)
        {
            playerAnimator.SetBool("IsRunning", true);
            playerVoice.playerWalk.pitch = 2f;
        }
        else
        {
            playerAnimator.SetBool("IsRunning", false);
            playerVoice.playerWalk.pitch = 1f;
        }
    }

    private void OnMove(InputValue value)
    {
        newMove = InputManager.Instance.GetMoveValue(value);

        playerVoice.playerWalk.Play();
        playerVoice.playerWalk.volume = 0.5f;

        if (newMove.magnitude > 1)
        {
            newMove.Normalize();
        }

        CameraAndMovingHandling();

        PlayerWalk();

    }

    private void CameraAndMovingHandling()
    {
        Vector3 Forward = Camera.main.transform.forward;
        Forward.y = 0f;
        Forward.Normalize();

        Vector3 Right = Camera.main.transform.right;
        Right.y = 0f;
        Right.Normalize();

        Vector3 moveDirection = (newMove.x * Right + newMove.y * Forward).normalized;

        playerRigidBody.velocity = moveDirection * playerSpeed;
    }
    private void PlayerWalk()
    {
        if (newMove.magnitude < 0.1f)
        {
            playerVoice.playerWalk.Stop();
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
    }


    public void IsGrounded() => Physics.CheckSphere(GroundCheck.position, GroundDistance, groundMask);

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("GhostLight"))
        {
            LightGhost[] ghostColliders = FindObjectsOfType<LightGhost>();
            Collider[] allChildrenColliders = ghostColliders.SelectMany(ghost => ghost.GetComponentsInChildren<Collider>()).ToArray();

            foreach (Collider childCollider in allChildrenColliders)
            {
                Physics.IgnoreCollision(playerCollider, childCollider);
            }

        }

        if (collision.gameObject.CompareTag("SafeRoom"))
        {
            foreach (Collider safeRoomDoor in GameManager.Instance.safeRoomDoor)
            {
                Physics.IgnoreCollision(playerCollider, safeRoomDoor);
                guard.gameObject.SetActive(false);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("SafeRoom"))
        {
            foreach (Collider safeRoomDoor in GameManager.Instance.safeRoomDoor)
            {
                Physics.IgnoreCollision(playerCollider, safeRoomDoor);
                guard.gameObject.SetActive(true);
            }
        }
    }

}
