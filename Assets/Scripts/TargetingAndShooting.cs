using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class TargetingAndShooting : MonoBehaviour
{
    private Vector3 dir;
    private float rotationSpeed = 10f;
    Ray RayOrigin;
    RaycastHit HitInfo;
    [SerializeField]Camera _camera;
    [SerializeField] GameObject launchPoint;
    [SerializeField] launchProjectile projectile;
    [SerializeField] ParticleSystem fireLaserBeams;
    private bool fireBool = false;

    private void Start()
    {        
        var em = fireLaserBeams.emission;
        em.enabled = false;
        fireLaserBeams.Play();
    }


    public void FireProjectile(InputAction.CallbackContext input)
    {

        var em = fireLaserBeams.emission;
        if (input.performed) // the key has been pressed
        {
            fireBool = true;
            em.enabled = true;
        }

        if (input.canceled) //the key has been released
        {
            em.enabled = false;
            fireBool = false;
        }
    }

    void FiringP()
    {
        var em = fireLaserBeams.emission;
        if (fireBool)
        {
            em.enabled = true;
        }
        else
        {
            em.enabled = false;
        }
        
    }

}
