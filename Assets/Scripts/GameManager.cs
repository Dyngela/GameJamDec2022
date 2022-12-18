using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour
{
    public bool isGamePaused = false;
    private float previousTimeScale = 0f;
    public static GameManager instance;

    [Header("Game State")]
    [SerializeField] private PlankPickup _plankPickup;
    [SerializeField] private Collider2D _exitColliderBlock;
    [SerializeField] private ExitZone _exitArea;
    [SerializeField] private TMP_Text _currentTimeText;
    private TimeSpan _currentTime;
    
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        _currentTime = TimeSpan.Zero;
    }

    // Update is called once per frame
    private void Update()
    {
        if (isGamePaused) return;
        
        // Update time
        _currentTime = _currentTime.Add(TimeSpan.FromSeconds(Time.deltaTime));
        if (_currentTimeText != null)
            _currentTimeText.text = _currentTime.ToString();
        
        
        if (Player.instance.sanity <= 0.0f)
        {
            //todo game over
            TogglePause();
            MenuManager.instance.MissionFailed("You went insane");
        }

        if (Player.instance.health <= 0.0f)
        {
            TogglePause();
            MenuManager.instance.MissionFailed("You just died");
        }

        if (Player.instance.playerCured)
        {
            // TODO game win
        }
    }

    private void TogglePause()
    {
        if (Time.timeScale > 0)
        {
            previousTimeScale = Time.timeScale;
            Time.timeScale = 0;
            isGamePaused = true;
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = previousTimeScale;
            isGamePaused = false;
        }
    }

    public void OnPlankPickup()
    {
        throw new System.NotImplementedException();
    }
}
