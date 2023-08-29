using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTransform : MonoBehaviour
{
    public bool axisYOnly = true;
    public Transform target;
    public bool targetIsPlayer = true;
    public GameManager.CameraTransform targetCamera = GameManager.CameraTransform.None;
    // Start is called before the first frame update
    void Start()
    {
        if (targetCamera != GameManager.CameraTransform.None) {
            if (targetCamera == GameManager.CameraTransform.BottomLeft_MainCam) {
                target = GameManager.mainCameraBottomLeft;
            } else if (targetCamera == GameManager.CameraTransform.Bottom_MainCam) {
                target = GameManager.mainCameraBottom;
            } else if (targetCamera == GameManager.CameraTransform.BottomRight_MainCam) {
                target = GameManager.mainCameraBottomRight;
            } else if (targetCamera == GameManager.CameraTransform.MiddleLeft_MainCam) {
                target = GameManager.mainCameraMiddleLeft;
            } else if (targetCamera == GameManager.CameraTransform.Middle_MainCam) {
                target = GameManager.mainCameraMiddle;
            } else if (targetCamera == GameManager.CameraTransform.MiddleRight_MainCam) {
                target = GameManager.mainCameraMiddleRight;
            } else if (targetCamera == GameManager.CameraTransform.TopLeft_MainCam) {
                target = GameManager.mainCameraTopLeft;
            } else if (targetCamera == GameManager.CameraTransform.Top_MainCam) {
                target = GameManager.mainCameraTop;
            } else if (targetCamera == GameManager.CameraTransform.TopRight_MainCam) {
                target = GameManager.mainCameraTopRight;
            }

            return;
        }
        if (targetIsPlayer) {
            target = GameManager.player.transform;
            return;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 targetPostition;
        if (axisYOnly) {
            targetPostition = new Vector3(target.position.x,
                                       this.transform.position.y,
                                       target.position.z);
        } else {
            targetPostition = target.position;
        }
        
        this.transform.LookAt(targetPostition);
    }
}
