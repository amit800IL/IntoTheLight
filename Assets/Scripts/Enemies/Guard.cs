using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


public class Guard : MonoBehaviour
{
    private float distance;

    [Header("General")]
    [SerializeField] private GameObject guard;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Light enemyLight;
    [SerializeField] private Vector3 offsetDistance;
    [SerializeField] private PlayerGhostAwake ghostAwake;

    [Header("Numbers")]
    [SerializeField] private float jumpToPlayerDistance;
    [SerializeField] private float speed;
    [SerializeField] private float speedMultiplier;
    [SerializeField] private float KillingDistance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource guardScream;
    [SerializeField] private AudioSource guardKillingScream;
    [SerializeField] private AudioSource guardFlySound;
    [SerializeField] private Animator guardAnimator;

    [Header("Renderers")]
    [SerializeField] private SkinnedMeshRenderer guardMeshRenderer;
    [SerializeField] private MeshRenderer guardWeaponMeshRenderer;


    private void Start()
    {
        guardMeshRenderer.forceRenderingOff = true;
        guardWeaponMeshRenderer.forceRenderingOff = true;

        StartCoroutine(CalculateRoute());
    }

    public IEnumerator CalculateRoute()
    {

        yield return new WaitForSeconds(5);


        guardFlySound.Play();

        while (true)
        {

            distance = Vector3.Distance(transform.position, GameManager.Instance.Player.transform.position);

            if (agent != null && distance <= enemyLight.range)
            {
                guardMeshRenderer.forceRenderingOff = true;
                guardWeaponMeshRenderer.forceRenderingOff = true;
                guardScream.Play();
                agent.SetDestination(GameManager.Instance.Player.transform.position);
                agent.updateRotation = true;

                if (distance <= KillingDistance && !ghostAwake.isInRangeOfGhost)
                {

                    guardMeshRenderer.forceRenderingOff = false;
                    guardWeaponMeshRenderer.forceRenderingOff = false;


                    GuardKill();

                    yield return new WaitForSeconds(3);

                    if (GameManager.Instance.PlayerStats.HP <= 0)
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    }
                }

                else
                {
                    guardAnimator.SetBool("IsAttacking", false);
                    GameManager.Instance.PlayerMovement.playerAnimator.SetBool("IsAttacked", false);
                    guardScream.pitch = 1f;
                }

            }
            else if (distance >= jumpToPlayerDistance)
            {
                yield return new WaitForSeconds(5);

                Vector3 offset = Random.onUnitSphere * speed + offsetDistance;

                offset.y = 0;

                agent.Warp(GameManager.Instance.Player.transform.position + offset);
            }
            yield return new WaitForSeconds(1);
        }
    }

    private void GuardKill()
    {
        Camera.main.transform.LookAt(transform.position);
        guardKillingScream.Play();
        guardScream.pitch = 2f;
        guardKillingScream.volume = 1f;
        Vector3 targetPosition = GameManager.Instance.Player.transform.position;
        targetPosition.y = transform.position.y;
        transform.LookAt(targetPosition);
        agent.isStopped = true;
        guardAnimator.SetBool("IsAttacking", true);
        GameManager.Instance.PlayerStats.HP -= 100;
        GameManager.Instance.playerScream.Play();
        GameManager.Instance.secondPlayerScream.Play();
        GameManager.Instance.PlayerMovement.playerAnimator.SetBool("IsAttacked", true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("GhostLight"))
        {
            StopCoroutine(CalculateRoute());
        }

        if (collision.gameObject.CompareTag("SafeRoom"))
        {
            StopCoroutine(CalculateRoute());
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Physics.IgnoreCollision(GameManager.Instance.playerCollider, GameManager.Instance.GuardCollider);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("GhostLight"))
        {
            StartCoroutine(CalculateRoute());
        }
    }

}
