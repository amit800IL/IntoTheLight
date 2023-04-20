using UnityEngine;

public class GameManager : MonoBehaviour
{

    [field: SerializeField] public static GameManager Instance { get; private set; }
    [field: SerializeField] public Transform Player { get; private set; }
    [field: SerializeField] public PlayerStats PlayerStats { get; private set; }
    [field: SerializeField] public Ghost Ghost { get; private set; }
    [field: SerializeField] public Collider Collider { get; private set; }

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
    public void Death(bool isGhostAwake)
    {
        bool raycast = Physics.Raycast(Ghost.transform.position, Player.position, distance);
        distance = Vector3.Distance(Ghost.transform.position, Player.position);

        if (distance > Ghost.Light.range && !isGhostAwake && raycast)
        {
            PlayerStats.HP--;

        }
        else if (distance < Ghost.Light.range && isGhostAwake && raycast)
        {
            PlayerStats.HP++;
        }

        Debug.Log(PlayerStats.HP);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(Ghost.transform.position, Player.transform.position);
    }
}
