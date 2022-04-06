using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [Tooltip ("Increase/Decrease speed of gun rotation")][SerializeField] float pitchFactor = -0.3f, yawFactor = 0.3f;
    [Tooltip("Limit of gun pitch rotation")] [SerializeField] float clampPitchMin = -50f, clampPitchMax = 10f;

    float yaw = 0;
    float pitch = 0;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ReadMousePosition(InputAction.CallbackContext input)
    {
        
        yaw = input.ReadValue<Vector2>().x * yawFactor + yaw;
        pitch = input.ReadValue<Vector2>().y * pitchFactor + pitch;
        float pitchClamped = Mathf.Clamp(pitch, clampPitchMin, clampPitchMax);

        transform.localRotation = Quaternion.Euler(pitchClamped, yaw, 0);
    }


}
