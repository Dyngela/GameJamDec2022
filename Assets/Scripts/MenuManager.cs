using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;
    public GameObject GameOver;
    public GameObject GameWin;
    public GameObject GameOverReasonText;
    public GameObject ButtonRetry;
    public GameObject ButtonExit;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        gameObject.SetActive(true);
        GameOver.SetActive(false);
        GameWin.SetActive(false);
    }

    public void MissionFailed(string message, bool buttonDisplay = true)
    {
        GameOver.SetActive(true);
        ButtonRetry.SetActive(buttonDisplay);
        ButtonExit.SetActive(buttonDisplay);
        
        GameOverReasonText.GetComponent<TextMeshProUGUI>().text = message;
    }

    public void Hide()
    {
        GameOver.SetActive(false);
    }
}
