using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Task = System.Threading.Tasks.Task;

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
    
    public void OnClickExit()
    {
        SceneManager.LoadScene("main menu");
    }

    public void OnRetryClick()
    {
        StartCoroutine(InitializeGame());
    }

    private IEnumerator InitializeGame()
    {
        Debug.Log("Initializing game");
        Player.instance.Reset();

        _plankPickup?.gameObject?.SetActive(true);
        _exitColliderBlock?.gameObject?.SetActive(true);
        _exitArea?.gameObject?.SetActive(false);
        
        _tilemapExitLocked?.SetActive(true);
        _tilemapExitUnlocked?.SetActive(false);
        
        _currentTime = TimeSpan.Zero;

        _hasPlank = false;
        _hasEscaped = false;
        
        MenuManager.instance.Hide();
        
        if (Time.timeScale == 0)
            TogglePause();
        yield return null;
    }

    // Update is called once per frame
    private void Update()
    {
        if (isGamePaused) return;
        
        // Update time
        _currentTime = _currentTime.Add(TimeSpan.FromSeconds(Time.deltaTime));
        _currentTimeText.text = _currentTime.ToString();

        if (Player.instance.sanity <= 0.0f)
        {
            TogglePause();
            MenuManager.instance.MissionFailed("You went insane");
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
        StartCoroutine(SendScore());
    }
    
    public IEnumerator SendScore()
    {
        MenuManager.instance.MissionFailed("Submitting your score...", false);
        
        ScoreEntry entry = new ScoreEntry
        {
            PlayerName = PlayerPrefs.GetString("playerName", "legent27"),
            PlayerGuid = "",
            Span = _currentTime.Ticks
        };
        
        Task task = Scoreboard.CreateScoreEntry(entry);
        yield return new WaitUntil(() => task.IsCompleted);
        
        Debug.Log("ScoreSent!");
        MenuManager.instance.MissionFailed("Score submitted!");
    }
}
