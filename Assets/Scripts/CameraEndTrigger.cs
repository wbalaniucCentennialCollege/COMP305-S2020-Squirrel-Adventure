using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraEndTrigger : MonoBehaviour
{
    public CinemachineVirtualCamera isoCam;
    public CinemachineVirtualCamera groupCam;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            // Swap virtual camera
            isoCam.Priority = 1;
            groupCam.Priority = 10;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Swap virtual camera
            isoCam.Priority = 10;
            groupCam.Priority = 1;
        }
    }
}
