using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerChangeCameraAndCheckpoint : MonoBehaviour
{
    public Transform newCameraBundle;
    public Transform newPlayerCheckpoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider otherCollider) {
        if (otherCollider.CompareTag("Player")) {
            print("change Cameras");
            GameManager.checkpointCameraBundle.gameObject.SetActive(false);

            GameManager.checkpointCameraBundle = newCameraBundle;
            GameManager.playerCheckpoint = newPlayerCheckpoint;

            GameManager.checkpointCameraBundle.gameObject.SetActive(true);
            
        }
    }
}
