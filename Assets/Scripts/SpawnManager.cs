using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField] GameObject[] fighters;
    [SerializeField] Transform[] spawnpoint;
    [SerializeField] float spawnDelay = 20f;


    public void CommandToSpawn()
    {
        StartCoroutine(SpawnAfterDestroy());
    }
    IEnumerator SpawnAfterDestroy()
    {
        yield return new WaitForSeconds(spawnDelay);
        foreach(GameObject f in fighters)
        {
            Instantiate(f);
        }
        
    }
    
}
