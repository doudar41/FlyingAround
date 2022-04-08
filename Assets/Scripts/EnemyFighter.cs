using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;


    public class EnemyFighter : MonoBehaviour
    {

        [SerializeField] int lives = 10;
        Player player;
        private bool fireBool;
        [SerializeField] Transform[] spawnPoints;
        [SerializeField] GameObject projectile;
        [SerializeField] float deltaFormant = 100f;
        [SerializeField] Light redLight;
        [SerializeField] GameObject fighter;
        VfxDestroyer vfxDestroyer;
        SpawnManager spawn;
        GameBase gamebase;
        float delta, currentIntensivity;


        [SerializeField] ParticleSystem explosion, finaleBlow;

        public event Action deathEnemy;

        void Start()
        {
            gamebase = FindObjectOfType<GameBase>();
            spawn = FindObjectOfType<SpawnManager>();
            vfxDestroyer = FindObjectOfType<VfxDestroyer>();
            deathEnemy = DeathEnemy;
            redLight.intensity = 0f;
            currentIntensivity = 0f;
            player = FindObjectOfType<Player>();
            StartCoroutine(Firing());
        }

        void Update()
        {
        if (player != null) return;
            
                transform.LookAt(player.transform);
                delta = Vector3.Distance(transform.position, player.transform.position);
                FireProjectile();
            

        }
        private void OnParticleCollision(GameObject other)
        {

            lives -= 1;
            if(explosion != null)
                {
                    explosion.transform.position = gameObject.transform.position;
                    explosion.Play();
                }

            if (lives<=0)
            {
                deathEnemy?.Invoke();
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
            GetComponent<BoxCollider>().enabled = false;
            gamebase.AddScore(100);
            spawn.CommandToSpawn();
            ParticleSystem vfx = Instantiate(finaleBlow, gameObject.transform);
            vfx.transform.parent = vfxDestroyer.transform;
            vfxDestroyer.eventHandler(vfx);
   
            Destroy(fighter);
            deathEnemy -= DeathEnemy;
    }



}

