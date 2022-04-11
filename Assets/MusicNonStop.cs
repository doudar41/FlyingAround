using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicNonStop : MonoBehaviour
{
    
    void Awake()
    {
        MusicNonStop[] musics =  FindObjectsOfType<MusicNonStop>();
        if (musics.Length > 1) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
