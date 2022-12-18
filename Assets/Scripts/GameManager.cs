using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isGamePaused = false;
    private float previousTimeScale = 0f;
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    private void Update()
    {
        if (isGamePaused) return;
        
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
}
