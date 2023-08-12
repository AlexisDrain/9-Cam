using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerWait : MonoBehaviour {

    public float timeToWait = 3f;
    public UnityEvent onTimeEnd;
    public bool resetWithPlayer = true;
    public bool startOnEnable = true;

    private float currentTimeWaiting = 0f;

    void Start() {
        if (startOnEnable) {
            StartCoroutine(Countdown());
        }
        if (resetWithPlayer) {
            GameManager.playerRevive.AddListener(ResetTrigger);
        }
    }
    private void OnEnable() {
        if (startOnEnable) {
            StartCoroutine(Countdown());
        }
    }

    public IEnumerator Countdown() {
        yield return new WaitForSeconds(timeToWait);
        onTimeEnd.Invoke();
    }

    void ResetTrigger() {
        if(startOnEnable) {
            StartCoroutine(Countdown());
        }
    }
}
