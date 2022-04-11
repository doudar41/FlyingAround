using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



public class TargetingAndShooting : MonoBehaviour
{
    private Vector3 dir;
    Ray RayOrigin;
    RaycastHit HitInfo;
    [SerializeField]Camera _camera;
    [SerializeField] GameObject launchPoint;
    [SerializeField] launchProjectile projectile;
    [SerializeField] ParticleSystem fireLaserBeams;
    [SerializeField] Light greenLight;
    [SerializeField] AudioClip[] soundOfLaser;
    AudioSource audioSource;
    private bool fireBool = false;
    float currentIntensivity;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        var em = fireLaserBeams.emission;
        em.enabled = false;
        fireLaserBeams.Play();
        StartCoroutine(FXToParticles());
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

    void SoundAndLightFX()
    {
        StartCoroutine(FireLight(0.1f, 10, 0));
        audioSource.PlayOneShot(soundOfLaser[Random.Range(0, soundOfLaser.Length - 1)]);
    }

    IEnumerator FXToParticles()
    {
        while (true)
        {
            if (fireBool) SoundAndLightFX();
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator FireLight(float duration, float startIntensivity, float targetIntensivity)
    {

        float startingCoroutine = 0.0f;
        while (startingCoroutine < duration)
        {
            currentIntensivity = Mathf.Lerp(startIntensivity, targetIntensivity, startingCoroutine / duration);

            greenLight.intensity = currentIntensivity;
            startingCoroutine += Time.deltaTime;
            yield return null;
        }

        greenLight.intensity = targetIntensivity;
    }



}
