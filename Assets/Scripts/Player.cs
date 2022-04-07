using System.Collections;using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] int lives = 100;
    [SerializeField] ParticleSystem explosion;
    [SerializeField] TextMeshProUGUI healthText;

    public event Action death;

    void Start()
    {
        healthText.text = lives.ToString();
        
    }

    private void OnEnable()
    {
        death = DestroyShip;
    }

    private void OnDisable()
    {
        death -= DestroyShip;
    }

    private void OnTriggerEnter(Collider other)
    {
        lives -= 1;
        if (lives <= 0) lives = 0;
        healthText.text = lives.ToString();
        if (lives <= 0)
        {
            death();
        }
    }

    void DestroyShip()
    {
        
        GetComponentInChildren<MeshRenderer>().enabled = false;
        explosion.Play();
        Destroy(gameObject, 1f);
    }
}
