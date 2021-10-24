using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManager : SingletonBehaviour<GameManager>
{
    //private string filePath;
    public Player player;
    public bool gameStarted;
    public bool gamePaused;
    public bool gameOvered;

    protected void OnEnable()
    {
        //PlayerData.Create();
    }

    private void Awake()
    {
        InitializeSingleton();
        ClearStates();

        EventHub.GameOvered += OnGameOvered;
        SceneManager.sceneLoaded += OnGameSceneLoaded;
    }

    private void ClearStates()
    {
        gameStarted = false;
        gameOvered = false;
        gamePaused = false;
    }

    private void OnGameSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.buildIndex == 1)
        {
            player = FindObjectOfType<Player>();
            player.Init();
            PrepareGameStart();
        }    
    }

    private void PrepareGameStart()
    {
        Invoke(nameof(StartRun), 2f);
        UiManager.Instance.StartGame();
        ClearStates();
    }

    private void StartRun()
    {
        gameStarted = true;
        gameOvered = false;
        gamePaused = false;
    }

    private void Update()
    {
        if(gameOvered)
            return;
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame(!gamePaused);
        }
    }
    
    public void LoadGameScene()
    {
        SceneManager.LoadSceneAsync("Game", LoadSceneMode.Additive);
    }

    private void OnGameOvered()
    {
        gameOvered = true;
    }

    private void PauseGame(bool isPause)
    {
        EventHub.OnGamePaused(isPause);
        Time.timeScale = isPause ? 0f : 1f;
    }

    public void ShowMainMenu()
    {
        SceneManager.UnloadSceneAsync("Game");
        UiManager.Instance.ShowMainMenu();
    }

    public void RestartGame()
    {
        StartCoroutine(Restart());
    }
    public IEnumerator Restart()
    {
        player.playerInfo.coins = 0;
        player.playerInfo.passedDistance = 0;
        SceneManager.UnloadSceneAsync("Game");
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadSceneAsync("Game", LoadSceneMode.Additive);
    }
    public void QuitGame()
    {
        Application.Quit();
    }


    //public void Save()
    //{
    //    BinaryFormatter bf = new BinaryFormatter();
    //    FileStream file = File.Create(filePath);

    //    PlayerData data = new PlayerData();

    //    data.coins = coins;
    //    //data.max = new int[2];
    //    //data.progress = new int[2];

    //    bf.Serialize(file, data);
    //    file.Close();
    //}

    //void Load()
    //{
    //    BinaryFormatter bf = new BinaryFormatter();
    //    FileStream file = File.Open(filePath, FileMode.Open);

    //    PlayerData data = (PlayerData)bf.Deserialize(file);
    //    file.Close();

    //    coins = data.coins;
    //}

   
}

//public abstract class AState : MonoBehaviour
//{
//    [HideInInspector]
//    public GameManager manager;

//    public abstract void Enter(AState from);
//    public abstract void Exit(AState to);
//    public abstract void Tick();

//    public abstract string GetName();
//}
////[Serializable]
////public class PlayerData
////{
////    public int coins;
////    //public int[] max;
////    //public int[] progress;
////}
