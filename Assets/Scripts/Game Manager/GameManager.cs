using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [field: SerializeField] public static GameManager Instance { get; private set; }
    [field: SerializeField] public Transform Player { get; private set; }
    [field: SerializeField] public PlayerStats PlayerStats { get; private set; }
    [field: SerializeField] public LightGhost[] Ghost { get; private set; }

    [SerializeField] private float distance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }

    }

    private void Start()
    {
        Ghost = FindObjectsOfType<LightGhost>();
    }
    public void Death(bool isGhostAwake)
    {

        foreach (LightGhost ghost in Ghost)
        {
            bool IsCloseGhost = Vector3.Distance(ghost.transform.position, Player.position) <= ghost.Light.range;

            bool IsFarGhost = Vector3.Distance(ghost.transform.position, Player.position) > ghost.Light.range;

            distance = Vector3.Distance(ghost.transform.position, Player.position);

            bool raycast = Physics.Raycast(ghost.transform.position, Player.position - ghost.transform.position, distance);


            if (!isGhostAwake && raycast && IsFarGhost)
            {
                while (distance > ghost.Light.range)
                {
                    PlayerStats.HP -= 0.001f;
                    Debug.Log(PlayerStats.HP);
                    break;
                }
            }
            else if (isGhostAwake && raycast && IsCloseGhost)
            {
                while (distance < ghost.Light.range)
                {
                    PlayerStats.HP += 0.1f;
                    Debug.Log(PlayerStats.HP);
                    break;
                }
            }

        }

    }

    private IEnumerator HealthDown()
    {
        yield return new WaitForSeconds(5);
        PlayerStats.HP -= 1;
        Debug.Log(PlayerStats.HP);
    }

    private IEnumerator Hee()
    {
        yield return new WaitForSeconds(5);
        PlayerStats.HP -= 1;
        Debug.Log(PlayerStats.HP);
    }
    private void OnDrawGizmos()
    {
        foreach (var ghost in Ghost)
        {
            Gizmos.DrawLine(ghost.transform.position, Player.transform.position - ghost.transform.position);
        }
    }
}
