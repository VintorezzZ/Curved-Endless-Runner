using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public Transform endPoint;
    void Start()
    {
        WorldController.Instance.onPlatformMovement += TryDellAndAddPlatform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TryDellAndAddPlatform()
    {
        if(transform.position.z < WorldController.Instance.minZ)
        {
            WorldController.Instance.worldBuilder.CreatePlatform();
            Destroy(gameObject);
        }        
    }

    private void OnDestroy()
    {
        WorldController.Instance.onPlatformMovement -= TryDellAndAddPlatform;
    }
}
