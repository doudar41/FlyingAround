using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateStars : MonoBehaviour
{
    float addYaw;

    void Update()
    {
        addYaw += 0.05f;
        transform.localRotation = Quaternion.Euler(-90, 0, addYaw);
    }


}
