using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowShipCoordinates : MonoBehaviour
{
    float lastXPosition, lastZPosition, addYaw;

    private void Start()
    {
        lastXPosition = transform.position.y;
       // lastZPosition = transform.position.z;
    }
    void Update()
    {
        addYaw += 0.1f;
        transform.localRotation = Quaternion.Euler(0, addYaw, 0);
    }

    private void LateUpdate()
    {
        if(transform.position.y > lastXPosition)
        {
           // Debug.Log("changing height");
        }
        lastXPosition = transform.position.y;
    }
}
