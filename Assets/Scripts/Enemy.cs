using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] int lives = 10;
    Transform player;
    private bool fireBool;
    [SerializeField]  Transform[] spawnPoints;
    [SerializeField] GameObject projectile;
    [SerializeField] float deltaFormant = 100f;
    [SerializeField] Light redLight;
    float delta, currentIntensivity;

    void Start()
    {

        redLight.intensity = 0f;
        currentIntensivity = 0f;
        player = FindObjectOfType<PlayerController>().transform;
        StartCoroutine(Firing());
    }

    void Update()
    {
        transform.LookAt(player);
        delta = Vector3.Distance(transform.position, player.position);
        FireProjectile();
    }



    void FireProjectile()
    {

        if (delta < deltaFormant) 
        {
            fireBool = true;
        }

        if (delta > deltaFormant) 
        {
            fireBool = false;
        }
    }

    IEnumerator Firing()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.3f);
            FiringP();
        }
    }

    void FiringP()
    {
        if (fireBool)
        {
            StartCoroutine(FadeLight(0.1f, 100, 0));
            foreach (Transform x in spawnPoints) Instantiate(projectile, x);
        }
    }

    IEnumerator FadeLight(float duration, float startIntensivity, float targetIntensivity)
    {

        float startingCoroutine = 0.0f;
        while (startingCoroutine < duration)
        {
            currentIntensivity = Mathf.Lerp(startIntensivity, targetIntensivity, startingCoroutine / duration);

            redLight.intensity = currentIntensivity;
            startingCoroutine += Time.deltaTime;
            yield return null;
        }

        redLight.intensity = targetIntensivity;
    }

}
