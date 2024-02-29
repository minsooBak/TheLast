using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class VirtualCameraController : MonoBehaviour
{
    public static VirtualCameraController instace; 
    public Transform v_CameraTarget;
    public Transform playerTransform;
    public CinemachineVirtualCamera VirtualCamera;
    CinemachineBrain brain;
    Camera mainCamera;
    public bool isVirtualCameraOn;
    Vector3 playerFoward;
    Vector3 cameraFoward;
    Vector3 crossProduct;
    Vector3 targetCameraPos;
    Vector3 cameraRot;
    Quaternion targetCameraRot;
    float cameraPos_x;
    float cameraPos_z;
    float dotProduct;
    float angle;
    float rad;

    private void Awake()
    {
        instace = this;
        v_CameraTarget = GetComponent<Transform>();
        VirtualCamera = GetComponent<CinemachineVirtualCamera>();
        brain = GetComponent<CinemachineBrain>();
        mainCamera = Camera.main;
        isVirtualCameraOn = false;
    }

    private void FixedUpdate()
    {

        if (Input.GetMouseButton(1)||Input.GetMouseButton(0))
        {
            brain.enabled = true;
            isVirtualCameraOn = true;
        }
        else
        {
            brain.enabled = false;
            isVirtualCameraOn = false;
        }
        //MoveCameraBackward();
    }
    public void MoveCameraBackward()
    {
        if(!isVirtualCameraOn) 
        {
            playerFoward = playerTransform.forward;
            cameraFoward = playerTransform.position-transform.position;
            cameraFoward.y = 0;
            playerFoward.y = 0;
            rad = cameraFoward.magnitude;
            crossProduct = Vector3.Cross(cameraFoward.normalized, playerFoward.normalized);
            dotProduct = Vector3.Dot(cameraFoward.normalized, playerFoward.normalized);
            angle =Vector3.Angle(cameraFoward.normalized, playerFoward.normalized)*(crossProduct.y>=0?1:-1);
            if (dotProduct < 0)
            {
                if(crossProduct.y < 0)
                {
                    angle = angle + 2;
                    targetCameraPos.x = rad * Mathf.Sin(angle*Mathf.Deg2Rad);
                    targetCameraPos.y = transform.localPosition.y;
                    targetCameraPos.z = -rad * Mathf.Cos(angle*Mathf.Deg2Rad);
                    cameraRot = transform.localRotation.eulerAngles;
                    targetCameraRot = Quaternion.Euler(cameraRot.x, -angle, 0);
                    transform.localPosition = targetCameraPos;
                    transform.localRotation = targetCameraRot;
                }
                else
                {
                    angle = angle - 2;
                    targetCameraPos.x = rad * Mathf.Sin(angle * Mathf.Deg2Rad);
                    targetCameraPos.y = transform.localPosition.y;
                    targetCameraPos.z = -rad * Mathf.Cos(angle * Mathf.Deg2Rad);
                    cameraRot = transform.localRotation.eulerAngles;
                    targetCameraRot = Quaternion.Euler(cameraRot.x, -angle, 0);
                    transform.localPosition = targetCameraPos;
                    transform.localRotation = targetCameraRot;
                }
                
            }
            else if(dotProduct>=0&&dotProduct<0.98f)
            {
                if (crossProduct.y < 0)
                {
                    angle = angle + 2;
                    targetCameraPos.x = rad * Mathf.Sin(angle * Mathf.Deg2Rad);
                    targetCameraPos.y = transform.localPosition.y;
                    targetCameraPos.z = -rad * Mathf.Cos(angle * Mathf.Deg2Rad);
                    cameraRot = transform.rotation.eulerAngles;
                    targetCameraRot = Quaternion.Euler(cameraRot.x, -angle, 0);
                    transform.localPosition = targetCameraPos;
                    transform.localRotation = targetCameraRot;
                }
                else
                {
                    angle = angle - 2;
                    targetCameraPos.x = rad * Mathf.Sin(angle * Mathf.Deg2Rad);
                    targetCameraPos.y = transform.localPosition.y;
                    targetCameraPos.z = -rad * Mathf.Cos(angle * Mathf.Deg2Rad);
                    cameraRot = transform.localRotation.eulerAngles;
                    targetCameraRot = Quaternion.Euler(cameraRot.x, -angle, 0);
                    transform.localPosition = targetCameraPos;
                    transform.localRotation = targetCameraRot;
                }
            }
            else
            {
                angle = 0;
                targetCameraPos.x = rad * Mathf.Sin(angle * Mathf.Deg2Rad);
                targetCameraPos.y = transform.localPosition.y;
                targetCameraPos.z = -rad * Mathf.Cos(angle * Mathf.Deg2Rad);
                cameraRot = transform.localRotation.eulerAngles;
                targetCameraRot = Quaternion.Euler(cameraRot.x, -angle, 0);
                transform.localPosition = targetCameraPos;
                transform.localRotation = targetCameraRot;

            }
        }
    }

}
