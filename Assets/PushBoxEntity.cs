using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBoxEntity : MonoBehaviour
{
    public Vector3 spawnPos;

    private Rigidbody myRigidbody;
    private AudioSource myAudioSource;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myAudioSource = GetComponent<AudioSource>();

        spawnPos = transform.position;

        GameManager.playerRevive.AddListener(ResetBox);
    }
    void ResetBox() {
        transform.position = spawnPos;
        myRigidbody.position = spawnPos;
    }
    // Update is called once per frame
    void Update() {
        if (myRigidbody.velocity.magnitude > 0.5f && myAudioSource.isPlaying == false) {
            myAudioSource.PlayWebGL();
        }
        if (myRigidbody.velocity.magnitude < 0.5f && myAudioSource.isPlaying == true) {
            myAudioSource.StopWebGL();
        }
    }
}
