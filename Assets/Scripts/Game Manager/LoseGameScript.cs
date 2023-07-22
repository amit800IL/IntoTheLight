using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class EndGameScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Waiting for death");
        gameObject.SetActive(false);
        PlayerStats.OnDeath += DeathSquenceMethods;
    }
    private void DeathSquenceMethods()
    {
        Debug.Log("Dead");
        DeathPanelActivate();
        Thread.Sleep(15000);
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
