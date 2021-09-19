using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldController : MonoBehaviour
{
    private static WorldController _instance;
    public static WorldController Instance
    {
        get
        {
            if (_instance == null)
                Debug.Log("WorldController is " + "null");
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;    
    }

    public delegate void TryToDellAndAddPlatform();
    public event TryToDellAndAddPlatform onPlatformMovement;

    public WorldBuilder worldBuilder;

    public float currentSpeed = 10f;
    public static float currSpeed;
    public float maxSpeed = 30f;
    public float speedUp;

    public float minZ = -5f;
    void Start()
    {        
        StartCoroutine(OnPlatformMovementCoroutine());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        StartCoroutine(StartGameIn());        
    }

    void Move()
    {
        if (PlayerController.gameOver == false && UIManager.GameIsPaused == false)
        {
            transform.Translate((-Vector3.forward) * currentSpeed * Time.fixedDeltaTime);
            currentSpeed += speedUp * Time.fixedDeltaTime;
            if (currentSpeed > maxSpeed)
                currentSpeed = maxSpeed;
            currSpeed = currentSpeed;
        }           
        
    }
    IEnumerator StartGameIn()
    {
        yield return new WaitForSeconds(2f);
        Move();
    }
    IEnumerator OnPlatformMovementCoroutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(1f);
            onPlatformMovement?.Invoke();
        }
    }
}
