using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : SingletonBehaviour<WorldController>
{
    public delegate void TryToDellAndAddPlatform();
    public event TryToDellAndAddPlatform onPlatformMovement;

    public WorldBuilder worldBuilder;

    public float currentSpeed = 10f;
    public float maxSpeed = 30f;
    public float speedUp;

    public float minZ = -5f;

    private void Awake()
    {
        InitializeSingleton();
    }

    void Start()
    {        
        StartCoroutine(PlatformBuildingRoutine());
    }

    private void Update()
    {
        if (GameManager.Instance.gameOvered || GameManager.Instance.gamePaused) 
            return;
        
        if(GameManager.Instance.gameStarted)
            Move();
    }

    private void Move()
    {
        transform.Translate((-Vector3.forward) * currentSpeed * Time.deltaTime);
        currentSpeed += speedUp * Time.deltaTime;
        if (currentSpeed > maxSpeed)
            currentSpeed = maxSpeed;

    }
  
    IEnumerator PlatformBuildingRoutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(1f);
            onPlatformMovement?.Invoke();
        }
    }
}
