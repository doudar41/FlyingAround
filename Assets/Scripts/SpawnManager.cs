using System.Collections;
using System.Collections.Generic;
using UnityEngine;using UnityEngine.Timeline;
using UnityEngine.Playables;

public class SpawnManager : MonoBehaviour
{

    [SerializeField] GameObject fighter;
    [SerializeField] GameObject[] spawnpoint;
    [SerializeField] float spawnDelay = 20f;
    [SerializeField] PlayableDirector[] timelines;
    [SerializeField] TimelineAsset[] waveTimeline;



    public void CommandToSpawn()
    {
        StartCoroutine(SpawnAfterDestroy());
    }
    IEnumerator SpawnAfterDestroy()
    {
        
        yield return new WaitForSeconds(spawnDelay);

/*        for(int i=0; i < timelines.Length; i++)
        {
            waveTimeline[i] = (TimelineAsset)timelines[i].playableAsset;
        }*/

        for (int i = 0; i < timelines.Length; i++)
        {
            foreach (var track in waveTimeline[i].outputs)
            {
                

                if (!timelines[i].GetGenericBinding(track.sourceObject)){
                    var animator = Instantiate(fighter, spawnpoint[i].transform);
                    timelines[i].SetGenericBinding(track.sourceObject, animator);
                }
                    

            }
        }
        
    }
    
}
