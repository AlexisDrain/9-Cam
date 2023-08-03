using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerShowTutorial : MonoBehaviour
{

    void OnTriggerEnter(Collider otherCollider) {
        if (otherCollider.CompareTag("Player")) {
            GameManager.canvasCrouchTutorial.SetActive(true);
        }

    }
}
