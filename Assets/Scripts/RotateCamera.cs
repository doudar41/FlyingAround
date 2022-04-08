using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    [SerializeField]float addPitch = -90,addYaw = 0, addRoll, addFactorPitch = 0f,addFactorYaw = 0f,addFactorRoll = 0.05f;

    void Update()
    {
        addPitch += addFactorPitch;
        addYaw += addFactorYaw;
        addRoll += addFactorRoll;
        transform.localRotation = Quaternion.Euler(addPitch, addYaw, addRoll);
    }


}
