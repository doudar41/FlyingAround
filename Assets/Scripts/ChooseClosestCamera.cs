using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ChooseClosestCamera : MonoBehaviour
{
    Player player;
    [SerializeField] Camera[] cameras;
    float distance;
    Camera chosenCamera;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        distance = Vector3.Distance(cameras[0].transform.position, player.transform.position);
        foreach (Camera c in cameras)
        {
            if (distance > Vector3.Distance(c.transform.position, player.transform.position))
            {
                distance = Vector3.Distance(c.transform.position, player.transform.position);
                chosenCamera = c;
            }
        }
       
    }
    private void Update()
    {
        if (player == null) return;
        foreach(Camera c in cameras)
        {
            if (distance > Vector3.Distance(c.transform.position, player.transform.position))
            {
                distance = Vector3.Distance(c.transform.position, player.transform.position);
                chosenCamera = c;
                chosenCamera.GetComponent<Camera>().enabled = true;
                foreach (Camera cam in cameras)
                {
                    if (cam != chosenCamera) cam.GetComponent<Camera>().enabled = false;
                }
            }
        }
    }


}
