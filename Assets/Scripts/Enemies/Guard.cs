using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


public class Guard : MonoBehaviour
{
    private float distance;
    [SerializeField] private GameObject guard;
    [SerializeField] private float jumpToPlayerDistance;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Light enemyLight;
    [SerializeField] private float speed;
    [SerializeField] private float speedMultiplier;
    [SerializeField] private AudioSource guardScream;
    [SerializeField] private AudioSource guardKillingScream;
    [SerializeField] private AudioSource guardFootSteps;
    [SerializeField] private MeshRenderer guardMesh;


    private void Start()
    {
        StartCoroutine(CalculateRoute());
    }
    public IEnumerator CalculateRoute()
    {

        guardMesh.forceRenderingOff = true;

        yield return new WaitForSeconds(1);

        guardFootSteps.Play();



        while (true)
        {
           
            distance = Vector3.Distance(transform.position, GameManager.Instance.Player.transform.position);

            if (agent != null && distance <= enemyLight.range)
            {
                guardScream.Play(); 
                agent.SetDestination(GameManager.Instance.Player.transform.position);
                agent.updateRotation = true;

                if (distance <= 3)
                {
                    guardMesh.forceRenderingOff = false;
                    guardKillingScream.Play();
                    guardScream.pitch = 2f;
                    guardKillingScream.volume = 1f;
                    yield return new WaitForSeconds(1);
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }

                else
                {
                    guardScream.pitch = 1f;
                }

            }
            else if (distance >= jumpToPlayerDistance)
            {
                yield return new WaitForSeconds(1);

                Vector3 offset = Random.onUnitSphere * speed;

                offset.y = 0;

                agent.Warp(GameManager.Instance.Player.transform.position + offset);
            }
            yield return new WaitForSeconds(2);
        }
    }
}
