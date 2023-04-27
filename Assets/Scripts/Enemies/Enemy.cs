using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    private float distance;
    [SerializeField] private NavMeshAgent agent;

    public void CalcluateRouteByLight(Light enemyLight)
    {
        bool raycast = Physics.Raycast(agent.transform.position, GameManager.Instance.Player.transform.position);
        distance = Vector3.Distance(agent.transform.position, GameManager.Instance.Player.transform.position);

        if (agent != null && raycast && distance < enemyLight.range)
        {
            Vector3 moveToPlayer = Vector3.MoveTowards(agent.transform.position, GameManager.Instance.Player.transform.position, distance);
            agent.destination = moveToPlayer;
            agent.updateRotation = true;

            if (distance <= 1f)
            {
                //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                Debug.Log("Player is dead");
            }
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawRay(agent.transform.position, GameManager.Instance.Player.transform.position);
    }
}
