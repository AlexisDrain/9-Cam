using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerDoSomethingInWorld : MonoBehaviour
{
    [TextArea(2, 30)]
    public string notes;
    public UnityEvent onTriggerEnter;
    public bool triggerWithPlayer = true;
    public bool resetWithPlayer = true;
    public bool resetOnEnable = true;
    public bool canBeReTriggered = false;

    private bool hasBeenTriggered = false;
    // Start is called before the first frame update
    void Start() {
        ResetTrigger();
        if (resetWithPlayer) {
            GameManager.playerRevive.AddListener(ResetTrigger);
        }
    }
    private void OnEnable() {
        if(resetOnEnable) {
            hasBeenTriggered = false;
        }
    }
    void ResetTrigger() {
        hasBeenTriggered = false;
    }
    void OnTriggerEnter(Collider otherCollider) {
        if (hasBeenTriggered && canBeReTriggered == false) {
            return;
        }
        if(triggerWithPlayer) {
            if (otherCollider.CompareTag("Player")) {
                onTriggerEnter.Invoke();
                hasBeenTriggered = true;
            }

        } else {
            onTriggerEnter.Invoke();
            hasBeenTriggered = true;
        }
    }
    private void OnCollisionEnter(Collision collision) {
        if (hasBeenTriggered && canBeReTriggered == false) {
            return;
        }
        if (triggerWithPlayer) {
            if (collision.collider.CompareTag("Player")) {
                onTriggerEnter.Invoke();
                hasBeenTriggered = true;
            }

        } else {
            onTriggerEnter.Invoke();
            hasBeenTriggered = true;
        }
    }
}
