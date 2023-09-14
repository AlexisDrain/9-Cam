using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventIntroExplodeWall : MonoBehaviour
{
    public GameObject oldWall;
    public GameObject newWall;
    public GameObject pipeKey;
    public GameObject oldPipe;
    public GameObject newPipe;

    public AudioSource steamAudioSource;
    public AudioClip explosionClip;
    public AudioClip steamClip;
    public AudioClip tapTurnClip;
    public AudioClip debrisClip;
    public ParticleSystem particles_smoke;
    public ParticleSystem particles_debris;

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

        if (otherCollider.CompareTag("Player")) {
            pipeKey.GetComponent<Animator>().SetTrigger("TurnPipeKey");
            GameManager.SpawnLoudAudio(tapTurnClip, Vector2.zero);
            steamAudioSource.clip = steamClip;
            steamAudioSource.PlayWebGL();
        }

        StartCoroutine(DelayExplosion());
    }

    public IEnumerator DelayExplosion() {
        yield return new WaitForSeconds(1.5f);
        particles_smoke.Play();
        yield return new WaitForSeconds(4f);
        oldWall.SetActive(false);
        oldPipe.SetActive(false);
        pipeKey.SetActive(false);

        newWall.SetActive(true);
        newPipe.SetActive(true);

        GameManager.SpawnLoudAudio(explosionClip, Vector2.zero, 0.5f);

        yield return new WaitForSeconds(0.1f);
        particles_debris.Play();

        GameManager.SpawnLoudAudio(debrisClip, Vector2.zero);
    }
}
