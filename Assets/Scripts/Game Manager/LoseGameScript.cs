using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseGameScript : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("Waiting for death");
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

        //StartCoroutine(CanvasFadeIn());
    }
    //private IEnumerator CanvasFadeIn()
    //{
    //    for (float i = 0; i <= 1; i = i + 0.01f)
    //    {
    //        gameObject.GetComponent<CanvasGroup>().alpha = i;
    //        yield return new WaitForSeconds(1);
    //    }
    //}
    private void RestartLevel()
    {
        Debug.Log("Restarted level");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
