using System.Collections;using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] int lives = 100;
    [SerializeField] ParticleSystem explosion;
    [SerializeField] TextMeshProUGUI healthText; //That's not good at all needs to be abstarc reference
    [SerializeField] Animator rail;
    [SerializeField]  AudioClip[] damageSoundClips;
    [SerializeField] GameObject cam;

    VfxDestroyer vfxDestroyer;
    GameBase gameBase;
    AudioSource damageSoundSource;
    
    event Action death;

    void Start()
    {
        rail.StopPlayback();
        damageSoundSource = GetComponent<AudioSource>();
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
        if (other.name == "Terrain") return;
        DamagePlayer();
    }

    private void OnParticleCollision(GameObject other)
    {
        DamagePlayer();
    }

    void DestroyShip()
    {
        GetComponent<Collider>().enabled = false;
        GetComponent<Animator>().enabled = false;
        gameBase.CallPlayerDeath(transform.position);
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

    void DamagePlayer()
    {
        StartCoroutine(ShakeCamera(0.1f, 1f));
        damageSoundSource.PlayOneShot(damageSoundClips[0]);
        lives -= 1;

        if (lives <= 0)
        {
            lives = 0;
            healthText.text = 0.ToString();
            death();
        }
        else healthText.text = lives.ToString();
    }

    IEnumerator ShakeCamera(float duration, float magnitude )
    {
        Quaternion _originalPosition = cam.transform.localRotation;
        float elapsed = 0;
        while (elapsed < duration)
        {
            float x = UnityEngine.Random.Range(-1.0f, 1.0f) * magnitude;
            float y = UnityEngine.Random.Range(-1.0f, 1.0f) * magnitude;
            float z = UnityEngine.Random.Range(-1.0f, 1.0f) * magnitude;
            cam.transform.localRotation = Quaternion.Euler( new Vector3(_originalPosition.x+x, _originalPosition.y+y, _originalPosition.z));
            elapsed += Time.deltaTime;
            yield return null;
        }
        cam.transform.localRotation = _originalPosition;
    }

}
