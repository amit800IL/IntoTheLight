using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    private float distance;
    [SerializeField] private float jumpToPlayerDistance;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Light enemyLight;
    [SerializeField] private float speed;

    private void Start()
    {
        StartCoroutine(CalcluateRoute());
    }
 
    private IEnumerator CalcluateRoute()
    {
        while (true)
        {
            distance = Vector3.Distance(transform.position, GameManager.Instance.Player.transform.position);

            if (agent != null && distance <= enemyLight.range)
            {
                agent.SetDestination(GameManager.Instance.Player.transform.position);
                agent.updateRotation = true;

                if (distance <= 0.5f)
                {
                    //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    Debug.Log("Player is dead");
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
