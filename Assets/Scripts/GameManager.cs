using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //public int coins;
    //private string filePath;

    protected void OnEnable()
    {
        //PlayerData.Create();
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        //filePath = Application.persistentDataPath + "/playerInfo.dat";

        //if (File.Exists(filePath));
        //    Load();
        
    }
    public void StartRun()
    {
        ScoreScript.coinsValue = 0;
        ScoreScript.distanceValue = 0;
        PlayerController.gameOver = false;
        SceneManager.LoadScene("Game");
    }

    public void EndRun()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Leaderboard()
    {
        SceneManager.LoadScene("Highscore");
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
