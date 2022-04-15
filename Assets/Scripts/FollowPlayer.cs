using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]PlayerController player;

    void Update()
    {
        if (player != null)
        {
            transform.position = new Vector3(player.transform.position.x, 100f, player.transform.position.z);
        }
    }
}
