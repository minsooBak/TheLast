using Cinemachine;
using UnityEngine;


public class VirtualCameraController : MonoBehaviour
{
    public Transform v_CameraTarget;
    public Transform vCamera;
    public Transform playerTransform;
    public CinemachineVirtualCamera VirtualCamera;
    CinemachineBrain brain;
    public bool isVirtualCameraOn;
    Vector3 playerFoward;
    Vector3 cameraFoward;
    Vector3 crossProduct;
    Vector3 targetCameraPos;
    Vector3 cameraRot;
    Quaternion targetCameraRot;
    int crossproductNum;
    float dotProduct;
    float angle;
    float rad;

    private void Awake()
    {
        v_CameraTarget = GetComponent<Transform>();
        VirtualCamera = GetComponent<CinemachineVirtualCamera>();
        brain = GetComponent<CinemachineBrain>();
        isVirtualCameraOn = false;
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(1) || Input.GetMouseButton(0))
        {
            vCamera.transform.position = transform.position;
            brain.enabled = true;
            isVirtualCameraOn = true;
        }
        else
        {
            brain.enabled = false;
            isVirtualCameraOn = false;
        }
    }
    public void MoveCameraBackward()
    {
        if (!isVirtualCameraOn)
        {
            
            playerFoward = playerTransform.forward;
            cameraFoward = playerTransform.position - transform.position;
            cameraFoward.y = 0;
            playerFoward.y = 0;
            rad = cameraFoward.magnitude;
            crossProduct = Vector3.Cross(cameraFoward.normalized, playerFoward.normalized);
            dotProduct = Vector3.Dot(cameraFoward.normalized, playerFoward.normalized);
            if(dotProduct<0)
            angle = Vector3.Angle(cameraFoward.normalized, playerFoward.normalized) * (crossProduct.y >= 0 ? 1 : -1);
            //if (dotProduct < 0)
            if (crossProduct.y < 0) crossproductNum = 1; else crossproductNum = -1;
            ChangeLocalPosAndRot(crossproductNum,dotProduct);
             
        }
    }
    private void ChangeLocalPosAndRot(int num,float dot)
    {
        if(dot>0.98f) angle = 0; else angle = angle + 2*num;
        targetCameraPos.x = rad * Mathf.Sin(angle * Mathf.Deg2Rad);
        targetCameraPos.y = transform.localPosition.y;
        targetCameraPos.z = -rad * Mathf.Cos(angle * Mathf.Deg2Rad);
        cameraRot = transform.localRotation.eulerAngles;
        targetCameraRot = Quaternion.Euler(cameraRot.x, -angle, 0);
        transform.SetLocalPositionAndRotation(targetCameraPos, targetCameraRot);
    }

}
