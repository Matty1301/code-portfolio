using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton   //Ensure no more than one instance exists

    public static LevelManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance.gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    public GameState currentGameState = GameState.PREGAME;

    private void Update()
    {
        if (currentGameState == GameState.PREGAME || currentGameState == GameState.POSTGAME)
            return;

        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    private void UpdateState(GameState state)
    {
        GameState previousGameState = currentGameState;
        currentGameState = state;

        switch (currentGameState)
        {
            case GameState.PREGAME:
                Time.timeScale = 1.0f;
                pausepanel.SetActive(false);
                break;

            case GameState.RUNNING:
                Time.timeScale = 1.0f;
                pausepanel.SetActive(false);
                break;

            case GameState.PAUSED:
                Time.timeScale = 0.0f;
                pausepanel.SetActive(true);
                break;

            case GameState.POSTGAME:
                Time.timeScale = 1.0f;
                pausepanel.SetActive(false);
                break;

            default:
                break;
        }
    }

    public void TogglePause()
    {
        if (currentGameState == GameState.RUNNING)
            UpdateState(GameState.PAUSED);

        else if (currentGameState == GameState.PAUSED)
            UpdateState(GameState.RUNNING);
    }

    public enum GameState
    {
        PREGAME, RUNNING, PAUSED, POSTGAME
    }
}
