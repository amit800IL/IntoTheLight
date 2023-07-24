using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseGameScript : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
        PlayerStats.OnDeath += DeathSquenceMethods;
    }
    private void OnEnable()
    {
        gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        PlayerStats.OnDeath -= DeathSquenceMethods;
    }
    private void DeathSquenceMethods()
    {
        DeathPanelActivate();
        StartCoroutine(deathTimer());
    }

    private IEnumerator deathTimer()
    {
        yield return new WaitForSeconds(3);
        RestartLevel();
    }
    private void DeathPanelActivate()
    {
        gameObject.SetActive(true);
    }

    private void RestartLevel()
    {
        Debug.Log("Restarted level");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
