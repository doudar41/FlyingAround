using System.Collections;using System;
using System.Collections.Generic;
using UnityEngine;

public class launchProjectile : MonoBehaviour
{
    Rigidbody rb;
    Transform gun;
    public bool player = false;
    [SerializeField]ParticleSystem explosion;

    void Start()
    {
        gun = GetComponentInParent<Transform>();
        rb = GetComponent<Rigidbody>();
        rb.AddForce(gun.right * -4000f);
        StartCoroutine(SelfDestroy());
        explosion.Play();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && !player)
        {
            Debug.Log("Hit");
            DestroyAfterParticle();
        }
        if (collision.gameObject.tag == "Enemy" && player)
        {
            Debug.Log("EnemyHit");
            DestroyAfterParticle();
        }
       
    }
    
    IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    void DestroyAfterParticle()
    {
        explosion.Play();

        while(explosion.isPlaying)
        {

        }
        Destroy(gameObject);
    }
}
