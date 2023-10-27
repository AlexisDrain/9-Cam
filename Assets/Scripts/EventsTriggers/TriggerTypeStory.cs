using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerTypeStory : MonoBehaviour
{
    public int storyIndexToType;
    public AudioClip startTypingAudio;
    public float waitBeforeTyping = 2f;

    public void TriggerStory() {
        StartCoroutine("TriggerStoryEnumerator");
    }

    IEnumerator TriggerStoryEnumerator() {
        yield return new WaitForSeconds(waitBeforeTyping);
        GameManager.SpawnLoudAudio(startTypingAudio);
        GameManager.canvasStoryType.StartTypewriter(storyIndexToType);
    }
}
