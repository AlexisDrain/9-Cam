using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerSequence : MonoBehaviour {

    public int totalSequenceNeeded = 4;
    public UnityEvent onSequenceComplete;
    public int currentSequenceInt = 0;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.playerRevive.AddListener(ResetTrigger);
    }
    public void IncrementSequenceInt() {
        currentSequenceInt += 1;

        if (currentSequenceInt >= totalSequenceNeeded) {
            currentSequenceInt = 0;
            onSequenceComplete.Invoke();
        }
    }
    public void ResetTrigger() {
        currentSequenceInt = 0;
    }

}
