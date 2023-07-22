using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrKeyText : MonoBehaviour
{
     
    private void Start()
    {
        this.GetComponent <TMP_Text>().text =
            GameManager.Instance.playerStats.CurrKeys.ToString();
    }

    private void Update()
    {
        this.GetComponent<TMP_Text>().text =
            GameManager.Instance.playerStats.CurrKeys.ToString();
    }
}
