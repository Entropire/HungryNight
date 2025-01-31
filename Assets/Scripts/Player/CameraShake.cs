using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CameraShake : MonoBehaviour
{
    public static CameraShake cam; // Static reference to the CameraShake instance
    public bool meheh = true; // Boolean flag to allow or prevent camera movement
    public Transform cameraPosition; // Reference to the camera's intended position

    public void Awake() => cam = this; // Assign this instance to the static variable on Awake

    public void OnShake(float duration, float strength)
    {
        meheh = false; // Disable normal camera movement during shake
        transform.DOShakePosition(duration, strength); // Apply positional shake effect
        transform.DOShakeRotation(duration, strength); // Apply rotational shake effect
        new WaitForSeconds(duration); // This line does nothing as is; should be used in a coroutine
        meheh = true; // Re-enable camera movement after shake
    }

    public static void Shake(float duration, float strength) => cam.OnShake(duration, strength); // Static method to trigger shake

    private void Update() 
    {
        if (meheh)
        {
            transform.position = cameraPosition.position; // Keep the camera at its designated position if not shaking
        }
    }
}
