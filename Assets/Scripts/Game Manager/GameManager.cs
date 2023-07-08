using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GhostManager GhostManager;

    [field: Header("Player Scripts Refernces")]
    [field: SerializeField] public Transform Player { get; private set; }
<<<<<<< HEAD
=======
    [field: SerializeField] public PlayerGhostAwake PlayerGhostAwake { get; private set; }
    [field: SerializeField] public PlayerStats playerStats { get; private set; }
>>>>>>> Fixes

    [field: Header("Colliders Refernces")]
    [field: SerializeField] public Collider[] safeRoomDoor { get; private set; }

    [field : SerializeField] public List<GameObject> enemies { get; private set; }

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
        SpawnEnemy();
    }
    public void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, enemies.Count);
        GameObject selectedEnemy = enemies[randomIndex];
        selectedEnemy.SetActive(true);
    }
}
