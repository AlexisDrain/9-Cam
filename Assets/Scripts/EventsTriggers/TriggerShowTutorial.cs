using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerShowTutorial : MonoBehaviour
{
    public bool crouchTutorial = true;
    public bool walkTutorial = false;
    public bool jumpTutorial = false;
    public bool hideTutorials = false;
    void OnTriggerEnter(Collider otherCollider) {
        if (otherCollider.CompareTag("Player")) {
            if (walkTutorial) {
                GameManager.canvasTopRightTutorial.SetActive(true);
            }
            if (crouchTutorial) {
                GameManager.canvasCrouchTutorial.SetActive(true);
            }
            if (jumpTutorial) {
                GameManager.canvasJumpTutorial.SetActive(true);
            }
            if (hideTutorials) {
                GameManager.canvasTopRightTutorial.SetActive(false);
                GameManager.canvasCrouchTutorial.SetActive(false);
                GameManager.canvasJumpTutorial.SetActive(false);
            }
        }

    }
}
