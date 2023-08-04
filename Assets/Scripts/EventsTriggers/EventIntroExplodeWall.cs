using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class EventIntroExplodeWall : MonoBehaviour
{
    public GameObject oldWall;
    public GameObject newWall;
    public GameObject pipeKey;
    public GameObject oldPipe;
    public GameObject newPipe;

    public AudioClip explosionClip;

    private bool hasBeenActivated = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider otherCollider)
    {
        if (hasBeenActivated) {
            return;
        }
        hasBeenActivated = true;

        print("Todo: Add pipe turn sfx. smoke sfx. explosion sf. Add smoke particle effect, explosion debree particle effect");
        if (otherCollider.CompareTag("Player")) {
            pipeKey.GetComponent<Animator>().SetTrigger("TurnPipeKey");
        }

        StartCoroutine(DelayExplosion());
    }

    public IEnumerator DelayExplosion() {
        yield return new WaitForSeconds(5);
        oldWall.SetActive(false);
        oldPipe.SetActive(false);
        pipeKey.SetActive(false);

        newWall.SetActive(true);
        newPipe.SetActive(true);

        GameManager.SpawnLoudAudio(explosionClip, Vector2.zero, 0.5f);
    }
}
