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
    float delta;

    void Start()
    {
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
            foreach(Transform x in spawnPoints)
            
            Instantiate(projectile, x);
        }

    }

}
