using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static UnityEngine.Events.UnityAction<GameState> gameStateChanged;
    public static UnityEngine.Events.UnityAction highScoreUpdated;

    private float gameWidth_Pixels;
    public float upperGameBoundX_Pixels { get; private set; }
    public float lowerGameBoundX_Pixels { get; private set; }
    public float gameBoundX { get; private set; }
    public float gameBoundY { get; private set; }

    public enum GameState
    {
        Pregame,
        Running,
        Paused,
        Postgame
    }
    public GameState currentGameState { get; private set; }
    public int finalScore { get; private set; }
    public int highScore { get; private set; }
    public int prevHighScore;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);

        //Set game bounds
        gameWidth_Pixels = Screen.height * 1440 / 2960; //Game aspect ratio 2960:1440
        upperGameBoundX_Pixels = Screen.width / 2 + gameWidth_Pixels / 2;
        lowerGameBoundX_Pixels = Screen.width / 2 - gameWidth_Pixels / 2;
        gameBoundX = Camera.main.ScreenToWorldPoint(new Vector2(upperGameBoundX_Pixels, 0)).x;
        gameBoundY = Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y;
    }

    private void Start()
    {
        UpdateHighScore(SaveFileManager.Instance.LoadHighScore());
    }

    private void UpdateHighScore(int newHighScore)
    {
        highScore = newHighScore;
        prevHighScore = highScore;
        highScoreUpdated?.Invoke();
    }

    //Called with true when app is suspended and false when app is resumed
    private void OnApplicationPause(bool pause)
    {
        if (currentGameState == GameState.Running && pause == true)
            TogglePause();
    }

    public void TogglePause()
    {
        if (currentGameState == GameState.Running)
            ChangeGameState(GameState.Paused);
        else if (currentGameState == GameState.Paused)
            ChangeGameState(GameState.Running);
    }

    private void ChangeGameState(GameState newGameState)
    {
        gameStateChanged?.Invoke(newGameState);

        if (newGameState == GameState.Paused)
            Time.timeScale = 0;
        else
            Time.timeScale = 1.0f;

        currentGameState = newGameState;
    }

    private void LoadScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public IEnumerator StartGame()
    {
        ChangeGameState(GameState.Running);
        LoadScene("Main");
        yield return null;  //Wait for the next frame to ensure the scene has loaded

        FindObjectOfType<LifeTracker>().SetStartingLives();
    }

    public IEnumerator GameOver()
    {
        ScoreTracker scoreTracker = FindObjectOfType<ScoreTracker>();   //scoreTracker is declared here
            //to ensure it is not used outside of this function as it will be null everywhere else
            //due to being destroyed when the scene changes

        ChangeGameState(GameState.Postgame);
        scoreTracker.StopIncrementScore();
        finalScore = scoreTracker.score;
        if (finalScore > highScore)
        {
            highScore = finalScore;
            SaveFileManager.Instance.SaveHighScore(highScore);
            PlayGamesPlatformManager.Instance.TryUploadScoreToLeaderboard(highScore);
        }
        yield return new WaitForSeconds(1);
        LoadScene("GameOver");
    }

    public void ReturnToMainMenu()
    {
        ChangeGameState(GameState.Pregame);
        LoadScene("MainMenu");
    }

    public void SyncLocalAndCloudSaves(int cloudSaveHighScore)
    {
        int localSaveHighScore = highScore;

        if (localSaveHighScore > cloudSaveHighScore)
            PlayGamesPlatformManager.Instance.TryUploadScoreToLeaderboard(localSaveHighScore);
        else if (cloudSaveHighScore > localSaveHighScore)
        {
            SaveFileManager.Instance.SaveHighScore(cloudSaveHighScore);
            UpdateHighScore(cloudSaveHighScore);
        }
    }
}
