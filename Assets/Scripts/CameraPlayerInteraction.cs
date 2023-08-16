using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayerInteraction : MonoBehaviour
{
    public bool followPlayerThroughWalls = false;
    private bool isLookingAtPlayer;
    // Start is called before the first frame update
    void Start()
    {
        // if (isTurret) {
        //     GameManager.canvasCrosshair.SetActive(true);
        // }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(followPlayerThroughWalls == true) {
            if(isLookingAtPlayer == false) {
                GetComponent<CinemachineVirtualCamera>().LookAt = GameManager.player.transform;
                isLookingAtPlayer = true;
            }
            return;
        } 

        RaycastHit hit;
        if(isLookingAtPlayer == false) {
            Physics.Linecast(transform.position, GameManager.player.transform.position, out hit, (1 << GameManager.worldMask)+ (1 << GameManager.entityMask));
            if(hit.collider && hit.collider.CompareTag("Player")) {
                GetComponent<CinemachineVirtualCamera>().LookAt = GameManager.player.transform;
                isLookingAtPlayer = true;
            }
        } else {
            Physics.Linecast(transform.position, GameManager.player.transform.position, out hit, (1 << GameManager.worldMask) + (1 << GameManager.entityMask));
            if (hit.collider && hit.collider.CompareTag("Player") == false) {
                GetComponent<CinemachineVirtualCamera>().LookAt = null;
                isLookingAtPlayer = false;
            }
        }
    }
}
