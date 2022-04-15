using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;


    public class Enemy : MonoBehaviour
    {

        [SerializeField] int lives = 10;
        Transform playerShip;
        private bool fireBool;
        [SerializeField] Transform[] spawnPoints;
        [SerializeField] GameObject projectile;
        [SerializeField] float deltaFormant = 100f;
        [SerializeField] Light redLight;
        VfxDestroyer vfxDestroyer;
        float delta, currentIntensivity;
        GameBase gameBase;

        [SerializeField] ParticleSystem explosion, smoke, fire, finaleBlow;

        public event Action deathEnemy;

        void Start()
        {

            vfxDestroyer = FindObjectOfType<VfxDestroyer>();
            gameBase = FindObjectOfType<GameBase>();
            deathEnemy = DeathEnemy;
            redLight.intensity = 0f;
            currentIntensivity = 0f;
            playerShip = FindObjectOfType<PlayerController>().transform;
            StartCoroutine(Firing());
            gameBase.deathOfPlayer += NullPlayer;
        }

        void NullPlayer(Vector3 player)
        {
            playerShip = null;
        }



    void Update()
        {
            if (playerShip != null)
            {
                transform.LookAt(playerShip);
                delta = Vector3.Distance(transform.position, playerShip.position);
                FireProjectile();
            }
        else { fireBool = false; return; }

    }
        private void OnParticleCollision(GameObject other)
        {
            lives -= 1;
            explosion.transform.position = gameObject.transform.position;
            explosion.Play();
        if (lives<=0)
        {
            deathEnemy?.Invoke(); ;
        }

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

    void DeathEnemy()
        {
        
        deathEnemy -= DeathEnemy;
        gameBase.AddScore(50, gameObject);
        VFXSpawner(smoke); VFXSpawner(fire); VFXSpawner(finaleBlow);
        Destroy(gameObject,0.1f);
        
        }


    void VFXSpawner(ParticleSystem ps)
        {
            ParticleSystem vfx = Instantiate(ps, gameObject.transform);
            vfx.transform.parent = vfxDestroyer.transform;
        }
}

