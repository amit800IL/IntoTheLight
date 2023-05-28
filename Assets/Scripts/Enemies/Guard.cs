using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


public class Guard : MonoBehaviour
{
    private float distance;
    [SerializeField] private float jumpToPlayerDistance;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Light enemyLight;
    [SerializeField] private float speed;
    [SerializeField] private float speedMultiplier;
    [SerializeField] private AudioSource guardScream;
    [SerializeField] private AudioSource guardKillingScream;

    private void Start()
    {
        StartCoroutine(CalcluateRoute());
    }

    private IEnumerator CalcluateRoute()
    {
        while (true)
        {
            guardScream.Play();
            guardScream.volume = 0.5f;
            distance = Vector3.Distance(transform.position, GameManager.Instance.Player.transform.position);

            if (agent != null && distance <= enemyLight.range)
            {
                agent.SetDestination(GameManager.Instance.Player.transform.position);
                agent.updateRotation = true;

                if (distance <= 1.5f)
                {
                    guardKillingScream.Play();
                    guardKillingScream.volume = 1f;
                    guardScream.volume = 1f;
                    yield return new WaitForSeconds(1);
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
                
            }
            else if (distance >= jumpToPlayerDistance)
            {
                yield return new WaitForSeconds(2);
                Vector3 offset = UnityEngine.Random.onUnitSphere * speed;

                offset.y = 0;

                agent.Warp(GameManager.Instance.Player.transform.position + offset);
            }
            yield return new WaitForSeconds(2);
        }
    }
}
