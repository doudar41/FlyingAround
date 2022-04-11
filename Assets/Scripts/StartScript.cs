using System.Collections;
using System.Collections.Generic;
using UnityEngine;using UnityEngine.Playables;

public class StartScript : MonoBehaviour
{
    [SerializeField] PlayableDirector maintimeline, startTimeline;
    [SerializeField] GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        startTimeline.Play();
        maintimeline.Stop();
    }

    // Update is called once per frame
    void Update()
    {

        if (startTimeline.time == startTimeline.duration)
        {
            Debug.Log("Are you in game");
            player.GetComponent<CapsuleCollider>().enabled = true;
            maintimeline.Play();
            Destroy(this);
        }
        
    }
}
