using System;
using UnityEngine;

[RequireComponent(typeof(MoveController))]
public class Player : MonoBehaviour
{
    public PlayerInfo playerInfo = new PlayerInfo();
    public MoveController moveController;

    private void Awake()
    {
        moveController = GetComponent<MoveController>();
    }

    private void Update()
    {
        if(GameManager.Instance.gameOvered || !GameManager.Instance.gameStarted)
            return;
        
        playerInfo.passedDistance += Time.deltaTime * WorldController.Instance.currentSpeed;
        moveController.Tick();
    }

    public void Init()
    {
        playerInfo.coins = 0;
        playerInfo.passedDistance = 0;
    }
}