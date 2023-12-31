using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictimHealth : MonoBehaviour
{
    public Transform victimSpawner;
    public int health = 3;
    public GameObject graphic;

    public AudioClip hurtAudioClip;

    void Start() {
        GameManager.playerRevive.AddListener(ResetVictim);
    }
    public void ResetVictim() {
        print("ResetVictim");
        health = 3;
        graphic.SetActive(true);
    }

    void OnTriggerEnter(Collider col) {

        if (col.CompareTag("Bullet")) {
            health -= 1;
            GetComponent<AudioSource>().clip = hurtAudioClip;
            GetComponent<AudioSource>().PlayWebGL();
            if (health <= 0) {
                GameObject gibs = GameManager.pool_Gibs.Spawn(transform.position);
                gibs.transform.rotation = transform.rotation;

                transform.position = victimSpawner.position;
                health = 3;
            }
        }
    }
    void OnCollisionEnter(Collision col) {

        if (col.collider.CompareTag("Bullet")) {
            health -= 1;
            GetComponent<AudioSource>().clip = hurtAudioClip;
            GetComponent<AudioSource>().PlayWebGL();
            if (health <= 0) {
                GameObject gibs = GameManager.pool_Gibs.Spawn(transform.position);
                gibs.transform.rotation = transform.rotation;

                transform.position = victimSpawner.position;
                health = 3;
            }
        }
    }
}
