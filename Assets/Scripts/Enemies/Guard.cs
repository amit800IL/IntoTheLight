using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    private float distance;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Light guardLight;

    private void Update()
    {
        CalcluateRoute();
    }

    public void CalcluateRoute()
    {
        bool raycast = Physics.Raycast(agent.transform.position, GameManager.Instance.Player.transform.position);
        distance = Vector3.Distance(agent.transform.position, GameManager.Instance.Player.transform.position);

        if (agent != null && raycast && distance < guardLight.range)
        {
            Vector3 moveToPlayerPos = Vector3.MoveTowards(agent.transform.position, GameManager.Instance.Player.transform.position, distance);
            agent.destination = moveToPlayerPos;
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawRay(agent.transform.position, GameManager.Instance.Player.transform.position);
    }
}
