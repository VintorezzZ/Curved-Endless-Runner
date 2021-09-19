using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBuilder : MonoBehaviour
{
    public GameObject[] freePlatform;
    public GameObject[] obstaclePlatform;
    public Transform platformContainer;

    private Transform _lastPlatform = null;

    private bool _isObstacle;
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init()
    {
        CreateFreePlatform(0);
        CreateFreePlatform();
        CreateFreePlatform();

        for (int i = 0; i < 10; i++)
        {
            CreatePlatform();
        }
    }

    public void CreatePlatform()
    {
        if (_isObstacle)
            CreateFreePlatform();
        else
            CreateObstaclePlatform();
    }

    
    private void CreateFreePlatform()
    {
        Vector3 pos = (_lastPlatform == null) ? platformContainer.position : _lastPlatform.GetComponent<PlatformController>().endPoint.position;
        
        int index = Random.Range(0, freePlatform.Length);
        GameObject result = Instantiate(freePlatform[index], pos, Quaternion.identity, platformContainer);
        _lastPlatform = result.transform;
        _isObstacle = false;
    }
    private void CreateFreePlatform(int index)
    {
        Vector3 pos = (_lastPlatform == null) ? platformContainer.position : _lastPlatform.GetComponent<PlatformController>().endPoint.position;
                        
        GameObject result = Instantiate(freePlatform[index], pos, Quaternion.identity, platformContainer);
        _lastPlatform = result.transform;
        _isObstacle = false;
    }

    private void CreateObstaclePlatform()
    {
        Vector3 pos = (_lastPlatform == null) ? platformContainer.position : _lastPlatform.GetComponent<PlatformController>().endPoint.position;


        int index = Random.Range(0, obstaclePlatform.Length);
        GameObject result = Instantiate(obstaclePlatform[index], pos, Quaternion.identity, platformContainer);
        _lastPlatform = result.transform;
        _isObstacle = true;
    }
}
