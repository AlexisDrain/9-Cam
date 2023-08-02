using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerDoSomethingInWorld : MonoBehaviour
{
    public UnityEvent onTriggerEnter;
    public bool triggerWithPlayer = true;
    public bool resetWithPlayer = true;

    private bool hasBeenTriggered = false;
    // Start is called before the first frame update
    void Start() {
        ResetTrigger();
        if (resetWithPlayer) {
            GameManager.playerRevive.AddListener(ResetTrigger);
        }
    }
    void ResetTrigger() {
        hasBeenTriggered = false;
    }
    void OnTriggerEnter(Collider otherCollider) {
        if (hasBeenTriggered) {
            return;
        }
        if(triggerWithPlayer) {
            if (otherCollider.CompareTag("Player")) {
                onTriggerEnter.Invoke();
                hasBeenTriggered = true;
            }

        }
    }
}
