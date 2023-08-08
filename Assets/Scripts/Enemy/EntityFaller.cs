using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Author: Alexis Clay Drain
*/
public class EntityFaller : MonoBehaviour {
    public float distanceFromPlayerToActivate = 65f;
    public AudioClip fallSFX;

    private bool isActive = false;
    private Vector3 startingPosition;

    private Rigidbody myRigidbody;

    void Start() {
        myRigidbody = GetComponent<Rigidbody>();
        
        isActive = false;
        startingPosition = transform.position;

        GameManager.playerRevive.AddListener(ResetEntity);
    }
    public void ResetEntity() {
        isActive = false;
        if (myRigidbody.isKinematic == false) {
            //Setting linear velocity of a kinematic body is not supported.
            myRigidbody.velocity = Vector3.zero;
        }
        myRigidbody.isKinematic = true;
        myRigidbody.useGravity = false;
        transform.rotation = Quaternion.identity;
        myRigidbody.rotation = Quaternion.identity;
        transform.position = startingPosition;
        myRigidbody.position = startingPosition;
        myRigidbody.velocity = Vector3.zero;
    }


    public void FixedUpdate() {

        if (isActive == false
            //&& GameManager.player.transform.position.z > transform.position.z - distanceFromPlayerToActivate) {
            && Vector3.Distance(GameManager.player.transform.position, transform.position) < distanceFromPlayerToActivate) {
                isActive = true;
            myRigidbody.isKinematic = false;
            myRigidbody.velocity = Vector3.zero;
            myRigidbody.useGravity = true;
            GameManager.SpawnLoudAudio(fallSFX);
        }
    }
}
