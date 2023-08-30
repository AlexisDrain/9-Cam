using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerChangeCameraAndCheckpoint : MonoBehaviour
{
    public Transform newCameraBundle;
    public Transform newPlayerCheckpoint;
    public bool isTurret = false;
    public bool isSomos = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ActivateTrigger() {
        print("change Cameras");
        GameManager.checkpointCameraBundle.gameObject.SetActive(false);

        GameManager.checkpointCameraBundle = newCameraBundle;
        GameManager.playerCheckpoint = newPlayerCheckpoint;

        GameManager.checkpointCameraBundle.gameObject.SetActive(true);

        // also done in GameManager new level
        GameManager.canvasTopRightTutorial.SetActive(false);
        GameManager.canvasCrouchTutorial.SetActive(false);
        GameManager.canvasJumpTutorial.SetActive(false);

        if (isTurret) {
            TurretCamManager.EnableMiddleCamera(isSomos);
        } else {
            TurretCamManager.DisableMiddleCamera();
        }
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider otherCollider) {
        if (otherCollider.CompareTag("Player")) {
            ActivateTrigger();
        }
    }
}
