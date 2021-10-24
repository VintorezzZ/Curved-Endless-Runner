using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : SingletonBehaviour<UiManager>
{
    [SerializeField] private Canvas leaderboardCanvas;
    [SerializeField] private Canvas inGameCanvas;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject deathUI;
    [SerializeField] private Text coinsText;
    [SerializeField] private Text distanceText;
    private PlayerInfo _playerInfo => GameManager.Instance.player.playerInfo;

    private void Awake()
    {
        InitializeSingleton();
        
        EventHub.GameOvered += OnGameOvered;
        EventHub.GamePaused += OnGamePaused;
        EventHub.ScoreChanged += UpdateScore; 
    }

    private void Update()
    {
        if(GameManager.Instance.gameStarted)
            distanceText.text = "Score: " + _playerInfo.passedDistance + "m";
    }

    private void UpdateScore()
    {
        coinsText.text = "Coins: " + _playerInfo.coins;
    }

    private void OnGameOvered()
    {
        deathUI.SetActive(true);
    }

    private void OnGamePaused(bool isPause)
    {
        pauseMenu.SetActive(isPause);
    }
    
    public void ShowLeaderboard(bool show)
    {
        leaderboardCanvas.enabled = show;
    }

    public void StartGame()
    {
        deathUI.SetActive(false);
        mainMenu.SetActive(false);
    }

    public void ShowMainMenu()
    {
        deathUI.SetActive(false);
        mainMenu.SetActive(true);
    }
}
