using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class VirtualCameraController : MonoBehaviour
{
    public Transform v_CameraTarget;
    CinemachineBrain brain;
    Camera mainCamera;
    bool isCameraOn;

    private void Awake()
    {
        v_CameraTarget = GetComponent<Transform>();
        brain = GetComponent<CinemachineBrain>();
        mainCamera = Camera.main;
        isCameraOn = false;
    }

    private void FixedUpdate()
    {
        transform.position = v_CameraTarget.transform.position;

        if (Input.GetMouseButton(1))
        {

            brain.enabled = true;
            isCameraOn = true;
        }

        else
        {
            brain.enabled = false;
            isCameraOn = false;
        }



    }

}
