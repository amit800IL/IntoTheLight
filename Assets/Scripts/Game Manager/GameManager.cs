using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [field: Header("Player Scripts Refernces")]
    [field: SerializeField] public Transform Player { get; private set; }
    [field: SerializeField] public PlayerGhostAwake PlayerGhostAwake { get; private set; }
    [field: SerializeField] public PlayerStats playerStats { get; private set; }

    [field: Header("Colliders Refernces")]
    [field: SerializeField] public GameObject enemy { get; private set; }

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

        Cursor.visible = false;
    }

    private void Start()
    {
        SpawnEnemy();
    }
    public void SpawnEnemy()
    {
        enemy.SetActive(true);
    }
}
