using UnityEngine;

public class GameManager : MonoBehaviour
{
    [field: SerializeField] public static GameManager Instance { get; private set; }
    [field: SerializeField] public Transform Player { get; private set; }
    [field: SerializeField] public PlayerStats PlayerStats { get; private set; }
    [field: SerializeField] public LightGhost Ghost { get; private set; }


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

        PlayerStats.HP = 100;
    }

    private void Update()
    {
        Death();
    }

    public void Death()
    {
        if (PlayerStats.HP <= 0)
        {
            Debug.Log("Player is dead");
        }

    }
}
