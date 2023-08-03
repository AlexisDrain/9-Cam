using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraBundle : MonoBehaviour
{
    
    void Start()
    {
        ResetCameras();
        GameManager.playerRevive.AddListener(ResetCameras);
    }

    public void ResetCameras() {
        //if(GameManager.checkpointCameraBundle == transform) {
        //    return;
        //}
        gameObject.SetActive(false);

        // this is handled in CameraPlayerInteraction
        /*
        for (int i = 0; i < transform.childCount; i += 1) {
            transform.GetChild(i).GetComponent<CinemachineVirtualCamera>().LookAt = GameManager.player.transform;
          //  //transform.GetChild(i).GetComponent<CinemachineVirtualCamera>().Follow = GameManager.player.transform;
        }
        */

        GameManager.checkpointCameraBundle.gameObject.SetActive(true);
    }

}
