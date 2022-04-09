using System.Collections;
using System.Collections.Generic;
using UnityEngine;using UnityEngine.Playables;

public class SpawnManager : MonoBehaviour
{

    [SerializeField] GameObject fighter;
    [SerializeField] PlayableDirector[] spawnpoint;
    [SerializeField] float spawnDelay = 20f;


    public void CommandToSpawn()
    {
        StartCoroutine(SpawnAfterDestroy());
    }
    IEnumerator SpawnAfterDestroy()
    {
        yield return new WaitForSeconds(spawnDelay);

        Instantiate(fighter);
        spawnpoint[0].SetGenericBinding(spawnpoint[0], fighter);

        
    }
    
}
