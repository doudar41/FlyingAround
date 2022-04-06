using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class lookahead : MonoBehaviour
{
    private Vector3 dir;
    private float rotationSpeed = 10f;
    Ray RayOrigin;
    RaycastHit HitInfo;
    [SerializeField]Camera _camera;
    [SerializeField] GameObject launchPoint;
    [SerializeField] launchProjectile projectile;
    private bool fireBool = false;

    private void Start()
    {
        StartCoroutine(Firing());
    }

    void Update()
    {
        Transform cameraTransform = _camera.transform;

        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out HitInfo, 300.0f))
            Debug.DrawRay(cameraTransform.position, cameraTransform.forward * 100.0f, Color.red);

        //if (HitInfo.collider.tag != null) { Debug.Log(HitInfo.collider.tag); }
    }

    public void FireProjectile(InputAction.CallbackContext input)
    {

        if (input.performed) // the key has been pressed
        {
            fireBool = true;
        }

        if (input.canceled) //the key has been released
        {
            fireBool = false;
        }
    }

    IEnumerator Firing()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            FiringP();
        }
    }

    void FiringP()
    {
        if (fireBool)
        {
            var x = Instantiate(projectile, launchPoint.transform);
            x.player = true;
        }
        
    }

}
