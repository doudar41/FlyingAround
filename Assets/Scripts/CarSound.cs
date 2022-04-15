using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class CarSound : MonoBehaviour
{

    [SerializeField]
    private AudioSource trackIdle, trackLouder;
    [SerializeField]
    float volumeFactor =0.5f, volumeFactorOffset;
    private float volumeCurrent = 0;
    private Vector3 changeRegister, changeSaver;

    void Start()
    {
        trackLouder.volume = volumeCurrent;
        trackIdle.Play();
        trackLouder.Play();
    }


    private void Update()
    {
        
        changeRegister = changeSaver - transform.position;
        //Debug.Log(changeRegister.magnitude);
        if (changeRegister.magnitude != 0)
        {
            double x =System.Math.Round(changeRegister.magnitude, 1);
            trackLouder.volume = (float)x*volumeFactor;
        }
        changeSaver = transform.position; 
    }
}
