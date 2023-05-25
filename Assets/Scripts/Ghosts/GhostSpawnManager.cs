using System.Collections;
using UnityEngine;

public class GhostSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject ghost;
    [SerializeField] private Transform[] spawnGhostLocations;
    [SerializeField] private float appearMaxDistance;
    [SerializeField] private float disappearMaxDistance;
    private bool isSpawning;
    private GameObject[] spawnedGhosts;

    private void Start()
    {
        isSpawning = false;
        spawnedGhosts = new GameObject[spawnGhostLocations.Length];
    
    }

    private void Update()
    {

        if (!isSpawning && (IsAnyGhostFarFromPlayer() || IsAllGhostsDestroyed()))
        {
            StartCoroutine(SpawnOrReloadGhost());
        }
    }

    private bool IsAnyGhostFarFromPlayer()
    {
        foreach (GameObject ghost in spawnedGhosts)
        {
            if (ghost != null)
            {
                float distance = Vector3.Distance(GameManager.Instance.Player.position, ghost.transform.position);
                if (distance <= disappearMaxDistance)
                {
                    return false;
                }
            }
        }
        return true;
    }

    private bool IsAllGhostsDestroyed()
    {
        foreach (GameObject ghost in spawnedGhosts)
        {
            if (ghost != null)
            {
                return false;
            }
        }
        return true;
    }

    private IEnumerator SpawnOrReloadGhost()
    {
        isSpawning = true;

        for (int i = 0; i < spawnGhostLocations.Length; i++)
        {
            if (spawnedGhosts[i] != null)
            {
                Destroy(spawnedGhosts[i]);
                spawnedGhosts[i] = null;
            }

            yield return new WaitForSeconds(1);

            float distance = Vector3.Distance(GameManager.Instance.Player.position, spawnGhostLocations[i].position);

            if (distance <= appearMaxDistance)
            {
                yield return new WaitForSeconds(1);
                spawnedGhosts[i] = Instantiate(ghost, spawnGhostLocations[i].position, spawnGhostLocations[i].rotation);
            }
        }

        isSpawning = false;
    }
}