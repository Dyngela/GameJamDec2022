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
    [SerializeField] private GameObject _tilemapExitLocked;
    [SerializeField] private GameObject _tilemapExitUnlocked;
    private TimeSpan _currentTime;
    private bool _hasPlank;
    private bool _hasEscaped;
    
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(InitializeGame());
    }

    private IEnumerator InitializeGame()
    {
        Debug.Log("Initializing game");
        Player.instance.transform.position = Vector3.zero;

        yield return new WaitForEndOfFrame();
        
        _plankPickup?.gameObject?.SetActive(true);
        _exitColliderBlock?.gameObject?.SetActive(true);
        _exitArea?.gameObject?.SetActive(false);
        
        _tilemapExitLocked?.SetActive(true);
        _tilemapExitUnlocked?.SetActive(false);
        
        _currentTime = TimeSpan.Zero;

        _hasPlank = false;
        _hasEscaped = false;
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
        if (_hasPlank) return;
        _hasPlank = true;
        
        Debug.Log("Player picked up plank");
        
        _plankPickup.gameObject.SetActive(false);
        _exitColliderBlock.gameObject.SetActive(false);
        _exitArea.gameObject.SetActive(true);
        
        _tilemapExitLocked.SetActive(false);
        _tilemapExitUnlocked.SetActive(true);
    }

    public void OnEscaped()
    {
        if (_hasEscaped) return;
        _hasEscaped = true;
        
        TogglePause();
    }
}
