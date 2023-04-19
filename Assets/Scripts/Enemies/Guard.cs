using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    private float distance;
    [SerializeField] private NavMeshAgent agent;

    private void Update()
    {
        CalcluateRoute();
    }

    public void CalcluateRoute()
    {
        bool raycast = Physics.Raycast(agent.transform.position, GameManager.Instance.player.transform.position);
        distance = Vector3.Distance(agent.transform.position, GameManager.Instance.player.transform.position);

        if (agent != null && raycast && distance < 5)
        {
            Vector3 moveToPlayerPos = Vector3.MoveTowards(agent.transform.position, GameManager.Instance.player.transform.position, distance);
            agent.destination = moveToPlayerPos;
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawRay(agent.transform.position, GameManager.Instance.player.transform.position);
    }
}
