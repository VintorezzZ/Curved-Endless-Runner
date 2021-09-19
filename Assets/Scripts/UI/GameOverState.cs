//using UnityEngine;
//using UnityEngine.UI;
//using System.Collections.Generic;
//using UnityEngine.SocialPlatforms.Impl;

///// <summary>
///// state pushed on top of the GameManager when the player dies.
///// </summary>
//public class GameOverState : AState
//{
//    public GameObject deathUI;

//    public Leaderboard miniLeaderboard;
//    //public Leaderboard fullLeaderboard;

//    public GameObject addButton;

//    public override void Enter(AState from)
//    {
//        deathUI.SetActive(true);

//        miniLeaderboard.playerEntry.inputName.text = PlayerData.instance.previousName;

//        miniLeaderboard.playerEntry.score.text = ScoreScript.distanceValue.ToString();
//        miniLeaderboard.Populate();
//    }

//    public override void Exit(AState to)
//    {
//        deathUI.gameObject.SetActive(false);
//        FinishRun();
//    }

//    public override string GetName()
//    {
//        return "GameOver";
//    }

//    public override void Tick()
//    {

//    }

//    //public void OpenLeaderboard()
//    //{
//    //    fullLeaderboard.forcePlayerDisplay = false;
//    //    fullLeaderboard.displayPlayer = true;
//    //    fullLeaderboard.playerEntry.playerName.text = miniLeaderboard.playerEntry.inputName.text;
//    //    fullLeaderboard.playerEntry.score.text = ScoreScript.distanceValue.ToString();

//    //    fullLeaderboard.Open();
//    //}      
//    protected void FinishRun()
//    {
//        if (miniLeaderboard.playerEntry.inputName.text == "")
//        {
//            miniLeaderboard.playerEntry.inputName.text = "Trash Cat";
//        }
//        else
//        {
//            PlayerData.instance.previousName = miniLeaderboard.playerEntry.inputName.text;
//        }

//        PlayerData.instance.InsertScore(ScoreScript.distanceValue, miniLeaderboard.playerEntry.inputName.text);
               
//        PlayerData.instance.Save();
//    }

//    //----------------
//}
