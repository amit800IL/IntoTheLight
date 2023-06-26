using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


public class Guard : MonoBehaviour
{
    private float distance;

    private Vector3 Returnposition;


    [field: Header("General")]
    public Vector3 OffsetDistance { get; private set; }
    public float Speed { get; private set; }

    public bool isChasingPlayer;
    [field: SerializeField] public Collider GuardCollider { get; private set; }
    [field: SerializeField] public NavMeshAgent agent { get; private set; }

    [SerializeField] private Light enemyLight;
    [SerializeField] private Vector3 offsetDistance;

    [Header("Numbers")]
    [SerializeField] private float jumpToPlayerDistance;
    [SerializeField] private float speed;
    [SerializeField] private float speedMultiplier;
    [SerializeField] private float KillingDistance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource guardScream;
    [SerializeField] private AudioSource guardKillingScream;
    [SerializeField] private AudioSource guardFlySound;

    [Header("Renderers")]
    [SerializeField] private MeshRenderer guardMeshRenderer;

    public Coroutine chaseCoroutine;
    private void Start()
    {
        guardMeshRenderer.forceRenderingOff = true;
        Returnposition = transform.position;
        chaseCoroutine = StartCoroutine(ChasePlayer());
    }

    private void Update()
    {
        Collide();
    }
    private void GoToPlayer()
    {
        Vector3 targetPosition = GameManager.Instance.Player.transform.position;
        targetPosition.y = transform.position.y;
        transform.LookAt(targetPosition);
        transform.Rotate(targetPosition);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * speedMultiplier * Time.deltaTime);
    }
    public IEnumerator ChasePlayer()
    {
        yield return new WaitForSeconds(5);
        isChasingPlayer = false;
        guardFlySound.Play();

        GameManager.Instance.PlayerVoice.PlayerOhNoScream.Play();
        yield return new WaitForSeconds(1);
        GameManager.Instance.PlayerVoice.PlayerOhNoScream.gameObject.SetActive(false);
        isChasingPlayer = true;


        while (isChasingPlayer)
        {
            distance = Vector3.Distance(transform.position, GameManager.Instance.Player.transform.position);

            if (agent != null && distance <= 5)
            {
                GameManager.Instance.PlayerVoice.GuardGettingCloser.Play();
                yield return new WaitForSeconds(1);
                GameManager.Instance.PlayerVoice.GuardGettingCloser.gameObject.SetActive(false);
                GoToPlayer();

                if (distance <= KillingDistance && !GameManager.Instance.PlayerGhostAwake.isInRangeOfGhost && isChasingPlayer)
                {

                    guardMeshRenderer.forceRenderingOff = false;

                    GuardKill();

                    yield return new WaitForSeconds(3);

                    if (GameManager.Instance.PlayerStats.HP <= 0)
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    }
                }

                else
                {
                    GameManager.Instance.PlayerMovement.playerAnimator.SetBool("IsAttacked", false);
                    guardScream.pitch = 1f;
                }

            }
            else if (distance >= jumpToPlayerDistance && isChasingPlayer)
            {
                GoToPlayer();
            }
            yield return new WaitForSeconds(1);

        }
    }

    private void GuardKill()
    {
        GameManager.Instance.PlayerVoice.GuardGettingCloser.Stop();
        GameManager.Instance.PlayerVoice.PlayerOhNoScream.Stop();
        Camera.main.transform.LookAt(transform.position);
        guardKillingScream.Play();
        guardScream.pitch = 2f;
        guardKillingScream.volume = 1f;
        Vector3 targetPosition = GameManager.Instance.Player.transform.position;
        targetPosition.y = transform.position.y;
        Camera.main.transform.LookAt(targetPosition);
        agent.isStopped = true;
        GameManager.Instance.PlayerStats.HP -= 100;
        GameManager.Instance.PlayerVoice.playerScream.Play();
        GameManager.Instance.PlayerVoice.secondPlayerScream.Play();
        GameManager.Instance.PlayerMovement.playerAnimator.SetBool("IsAttacked", true);
    }

    private void Collide()
    {
        LightGhost[] ghostColliders = FindObjectsOfType<LightGhost>();
        Collider[] allChildrenColliders = ghostColliders.SelectMany(ghost => ghost.GetComponentsInChildren<Collider>()).ToArray();

        foreach (Collider collider in allChildrenColliders)
        {
            if (Vector3.Distance(collider.gameObject.transform.position, GuardCollider.transform.position) <= 0.1f && GameManager.Instance.PlayerGhostAwake.HasAwaknedGhost && GameManager.Instance.PlayerGhostAwake.HasAwaknedGhost)
            {
                isChasingPlayer = false;
                agent.isStopped = true;
            }
        }
    }

        private void OnCollisionEnter(Collision collision)
        {


            if (collision.gameObject.CompareTag("SafeRoom"))
            {
                Debug.Log("dfdfdfdfdfdf");
                isChasingPlayer = false;
                agent.isStopped = true;
            }

            if (collision.gameObject.CompareTag("Player"))
            {
                Physics.IgnoreCollision(GameManager.Instance.PlayerMovement.playerCollider, GuardCollider);
            }
        }

    }
