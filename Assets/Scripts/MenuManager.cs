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

    // Update is called once per frame
    public void MissionFailed(string message)
    {
        GameOver.SetActive(true);
        GameOverReasonText.GetComponent<TextMeshProUGUI>().text = message;
    }

    public void Hide()
    {
        GameOver.SetActive(false);
    }
}
