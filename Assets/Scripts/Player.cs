using System.Collections;using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] int lives = 100;
    [SerializeField] ParticleSystem explosion;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] Animator rail;
    VfxDestroyer vfxDestroyer;
    GameBase gameBase;

    event Action death;

    void Start()
    {
        vfxDestroyer = FindObjectOfType<VfxDestroyer>();
        gameBase = FindObjectOfType<GameBase>();
        healthText.text = lives.ToString();
    }

    private void OnEnable()
    {
        death += DestroyShip;
    }

    private void OnDisable()
    {
        death -= DestroyShip;
    }

    private void OnTriggerEnter(Collider other)
    {
        lives -= 1;
        
        if (lives <= 0)
        {
            lives = 0;
            healthText.text = 0.ToString();
            death();
        }
        else healthText.text = lives.ToString();

    }

    void DestroyShip()
    {
        GetComponent<Collider>().enabled = false;
        GetComponent<Animator>().enabled = false;
        gameBase.CallDeath(gameObject.transform.position);
        rail.StopPlayback();
        if (vfxDestroyer != null)
        {

            ParticleSystem vfx = Instantiate(explosion, gameObject.transform);
            vfx.transform.parent = vfxDestroyer.transform;
            vfxDestroyer.eventHandler(vfx);
        }

        var x = GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer i in x)
        {
            i.enabled = false;
        }


        Destroy(gameObject);
    }
}
