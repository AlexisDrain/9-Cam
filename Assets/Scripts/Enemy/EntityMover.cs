using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* Author: Alexis Clay Drain
*/
public class EntityMover : MonoBehaviour
{
    public float startDelay = 1f;
    public float restartDelayDefault = 1f;
    public float moveSpeedToGoalPos = 10f;
    public float moveSpeedToOriginalPos = 2f;
    public Transform destinationTransform;

    private float startDelayCurrent = 0f;
    private float restartDelayCurrent = 0f;
    private Vector3 pos1;
    private Vector3 pos2;
    private bool goTo2 = true;

    void Start() {
        startDelayCurrent = startDelay;
        pos1 = transform.position;
        pos2 = destinationTransform.position;

        //GameManagerChasm.resetEnemyCollisions.AddListener(ResetEntity);
    }
    public void ResetEntity() {
        startDelayCurrent = startDelay;
        goTo2 = true;
        transform.position = pos1;
    }


    public void FixedUpdate() {

        if (startDelayCurrent > 0f) {
            startDelayCurrent -= 0.1f;
            return;
        }
        if (restartDelayCurrent > 0f) {
            restartDelayCurrent -= 0.1f;
            return;
        }

        if (goTo2 == true && Vector3.Distance(transform.position, pos2) >= 0.2f) {
            transform.position = Vector3.MoveTowards(transform.position, pos2, Time.deltaTime * moveSpeedToGoalPos);
        } else if (goTo2 == true) {
            restartDelayCurrent = restartDelayDefault;
            goTo2 = false;
        }
        if (goTo2 == false && Vector3.Distance(transform.position, pos1) >= 0.2f) {
            transform.position = Vector3.MoveTowards(transform.position, pos1, Time.deltaTime * moveSpeedToOriginalPos);
        } else {
            goTo2 = true;
        }
    }
}
