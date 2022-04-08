using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class VfxDestroyer : MonoBehaviour
{
    public event Action<ParticleSystem> destroyVFX;


    private void OnEnable()
    {
        destroyVFX += DestroyVFX;
    }


    public void eventHandler(ParticleSystem ps)
    {
        destroyVFX(ps);
    }
    void DestroyVFX(ParticleSystem ps)
    {
        Destroy(ps, 1f);
    }


    
}
