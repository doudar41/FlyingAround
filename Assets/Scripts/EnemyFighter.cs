using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;


    public class EnemyFighter : MonoBehaviour
    {

        [SerializeField] int lives = 10, scorePoints = 10;
        GameObject playerShip;
        private bool fireBool = false, startCoroutine =  true;
        [SerializeField] Transform[] spawnPoints;
        [SerializeField] GameObject projectile;
        [SerializeField] float deltaFormant = 100f, fireDelay = 0.3f;
        [SerializeField] Light redLight;
        [SerializeField] GameObject fighter;
        VfxDestroyer vfxDestroyer;
        GameBase gamebase;
        float delta, currentIntensivity;


        [SerializeField] ParticleSystem explosion, finaleBlow;

        public event Action deathEnemy;

        void Start()
        {
            gamebase = FindObjectOfType<GameBase>();
            vfxDestroyer = FindObjectOfType<VfxDestroyer>();
            deathEnemy = DeathEnemy;
            redLight.intensity = 0f;
            currentIntensivity = 0f;
            playerShip = GameObject.FindGameObjectWithTag("Player");
            gamebase.deathOfPlayer += NullPlayer;
        }


        void NullPlayer(Vector3 player)
        {
            playerShip = null;
        }
    void Update()
        {

        if (playerShip == null) { fireBool = false; return; }
            
                transform.LookAt(playerShip.transform);
                delta = Vector3.Distance(transform.position, playerShip.transform.position);
                FireProjectile();
        }
        private void OnParticleCollision(GameObject other)
        {

            lives -= 1;
            if(explosion != null)
            {
                ParticleSystem vfx = Instantiate(explosion, gameObject.transform);
                vfx.transform.parent = vfxDestroyer.transform;
                vfxDestroyer.eventHandler(vfx);
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
                if (startCoroutine)
                {
                    StartCoroutine(Firing());
                    startCoroutine = false;
                }
                
            }

            if (delta > deltaFormant)
            {
                fireBool = false;
                startCoroutine = true;
            }
        }

        IEnumerator Firing()
        {
        
            while (true)
            {
                yield return new WaitForSeconds(fireDelay);
                SpawnProjectile();
            }
        }

        void SpawnProjectile()
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
            gamebase.AddScore(scorePoints, fighter);
            GetComponent<BoxCollider>().enabled = false;
            ParticleSystem vfx = Instantiate(finaleBlow, gameObject.transform);
            vfx.transform.parent = vfxDestroyer.transform;
            
            
            Destroy(fighter);
         }


        public int GetBossHealth()
        {
            return lives;
        }

}

