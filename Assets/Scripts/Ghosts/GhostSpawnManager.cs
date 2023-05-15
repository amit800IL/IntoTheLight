using System.Collections;
using UnityEngine;

public class GhostSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject ghost;
    [SerializeField] private Transform[] spawnLocation;
    [SerializeField] private float appearMaxDistance;
    [SerializeField] private float dissapearMaxDistance;
    [SerializeField] private LightGhost[] lightGhosts;
    private bool isSpawning;

    private void Start()
    {
        lightGhosts = FindObjectsOfType<LightGhost>();
    }
    private void Update()
    {
        if (!isSpawning)
            StartCoroutine(SpwanPrefabs());
    }

    private IEnumerator SpwanPrefabs()
    {
        isSpawning = true;

        foreach (Transform spawnPoint in spawnLocation)
        {
            foreach (LightGhost lightGhost in lightGhosts)
            {
                float distance = Vector3.Distance(GameManager.Instance.Player.position, spawnPoint.position);


                if (distance >= appearMaxDistance)
                {
                    yield return new WaitForSeconds(2);
                    Instantiate(lightGhost, spawnPoint.position, spawnPoint.rotation);
                }

                if (distance > dissapearMaxDistance)
                {
                    yield return new WaitForSeconds(2);
                    Destroy(lightGhost);
                }

                yield return new WaitForSeconds(2);
            }
        }

        isSpawning = false;
    }
}
