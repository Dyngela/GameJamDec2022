using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private CanvasRenderer MainMenuPanelRef;
    [SerializeField] private CanvasRenderer ScoreboardPanelRef;
    [SerializeField] private CanvasRenderer RegisterFormRef;
    [SerializeField] private TMP_InputField NameInputRef;
    [SerializeField] private TMP_Text ScoreFirst;
    [SerializeField] private TMP_Text ScoreSecond;
    [SerializeField] private TMP_Text ScoreThird;
    private CanvasRenderer _activeCanvas;
    
    private void Start()
    {
        MainMenuPanelRef.gameObject.SetActive(false);
        ScoreboardPanelRef.gameObject.SetActive(false);
        RegisterFormRef.gameObject.SetActive(false);
        
        Scoreboard.GetScoreboard();
        
        if (PlayerPrefs.HasKey("playerName"))
            OnShowMainMenu();
        else
            OnShowRegisterForm();
    }


    public void OnSetPlayerName()
    {
        if (string.IsNullOrEmpty(NameInputRef.text)) return;
        PlayerPrefs.SetString("playerName", NameInputRef.text);
        
        OnShowMainMenu();
    }
    
    public void OnQuit()
    {
        Application.Quit();
    }

    public void OnPlay()
    {
        SceneManager.LoadScene("test");
    }

    public void OnShowMainMenu()
    {
        _activeCanvas?.gameObject.SetActive(false);
        MainMenuPanelRef.gameObject.SetActive(true);
        _activeCanvas = MainMenuPanelRef;
    }

    public void OnShowRegisterForm()
    {
        _activeCanvas?.gameObject.SetActive(false);
        RegisterFormRef.gameObject.SetActive(true);
        _activeCanvas = RegisterFormRef;
    }

    public void OnShowLeaderboard()
    {
        _activeCanvas?.gameObject.SetActive(false);
        ScoreboardPanelRef.gameObject.SetActive(true);
        _activeCanvas = ScoreboardPanelRef;

        RefreshScore();
    }

    public async Task RefreshScore()
    {
        List<ScoreRecord> scores = await Scoreboard.GetScoreboard();
        if (scores == null) return;
        Debug.Log("Refreshing scoreboard");

        if (scores.Count > 0)
            ScoreFirst.text = $"1. {scores[0].Span} - {scores[0].PlayerName}";
        if (scores.Count > 1)
            ScoreSecond.text = $"2. {scores[1].Span} - {scores[1].PlayerName}";
        if (scores.Count > 2)
            ScoreThird.text = $"3. {scores[2].Span} - {scores[2].PlayerName}";
    }
}
