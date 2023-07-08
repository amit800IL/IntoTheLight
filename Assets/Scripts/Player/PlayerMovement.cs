using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [field: Header("General")]

    private Vector3 newMove;
    [field: SerializeField] public Collider playerCollider { get; private set; }

    [SerializeField] private InputActionsSO InputActions;

    [SerializeField] private Rigidbody playerRigidBody;

    private bool isMoving = false;

    [Header("Ground Check")]

    [SerializeField] private float GroundDistance;
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private LayerMask groundMask;


    private void OnEnable()
    {
        InputActions.Enable();
        InputActions.Move.performed += OnMove;
        InputActions.Move.canceled += OnMove;
    }

    private void OnDisable()
    {
        InputActions.Disable();
        InputActions.Move.performed -= OnMove;
        InputActions.Move.canceled -= OnMove;
    }

    private void Update()
    {
        IsGrounded();
    }

    private void FixedUpdate()
    {
        if (newMove != Vector3.zero)
        {
            CameraAndMovingHandling();
        }
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        newMove = context.ReadValue<Vector2>();

        newMove = new Vector3(newMove.x, 0, newMove.y);

        if (newMove.magnitude > 1)
        {
            newMove.Normalize();
        }

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

        Vector3 moveDirection = newMove.x * Right + newMove.z * Forward;
        playerRigidBody.velocity = moveDirection * GameManager.Instance.playerStats.speed;
    }

    private void PlayerWalk()
    {
        if (newMove.magnitude < 0.1f)
        {
            isMoving = false;
            PlayerVoiceManager.Instance.playerWalk.Stop();
        }
        else
        {
            CameraAndMovingHandling();
            isMoving = true;
            PlayerVoiceManager.Instance.playerWalk.Play();
            PlayerVoiceManager.Instance.playerWalk.volume = 0.5f;
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
            }
        }
    }

}