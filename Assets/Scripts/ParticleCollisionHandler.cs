using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollisionHandler : MonoBehaviour
{
    [SerializeField] ParticleSystem explosion;

    private void OnParticleCollision(GameObject other)
    {
        explosion.transform.position = gameObject.transform.position;
        explosion.Play();

    }

}
