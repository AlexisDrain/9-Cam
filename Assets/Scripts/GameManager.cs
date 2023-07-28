using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static Vector3 playerReviveLocation;
    public static Quaternion playerReviveRotation;

    public static GameObject gameManagerObj;
    private static Pool pool_LoudAudioSource;
    public static GameObject player;

    public static bool playerIsAlive = true;

    public static UnityEvent playerRevive = new UnityEvent(); 

    void Awake()
    {
        gameManagerObj = gameObject;
        pool_LoudAudioSource = transform.Find("Pool_LoudAudioSource").GetComponent<Pool>();
        player = GameObject.Find("Player");
        playerReviveLocation = player.transform.position;
        playerReviveRotation = player.transform.rotation;
    }
    public static void KillPlayer() {
        print("player must die");
        playerIsAlive = false;
    }
    public static void RevivePlayer() {
        print("Revive Player");
        playerIsAlive = true;
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.GetComponent<Rigidbody>().rotation = playerReviveRotation;
        player.transform.rotation = playerReviveRotation;
        player.GetComponent<Rigidbody>().position = playerReviveLocation;
        player.transform.position = playerReviveLocation;
        playerRevive.Invoke();
    }

    public static AudioSource SpawnLoudAudio(AudioClip newAudioClip, Vector2 pitch = new Vector2(), float newVolume = 1f) {

        float sfxPitch;
        if (pitch.x <= 0.1f) {
            sfxPitch = 1;
        } else {
            sfxPitch = Random.Range(pitch.x, pitch.y);
        }

        AudioSource audioObject = pool_LoudAudioSource.Spawn(new Vector3(0f, 0f, 0f)).GetComponent<AudioSource>();
        audioObject.GetComponent<AudioSource>().pitch = sfxPitch;
        audioObject.PlayWebGL(newAudioClip, newVolume);
        return audioObject;
        // audio object will set itself to inactive after done playing.
    }
    public void Update() {
        if(playerIsAlive == false) {
            if (Input.GetButtonDown("Revive") || Input.GetButtonDown("Jump")) {
                RevivePlayer();
            }
        } else {
            if (Input.GetButtonDown("Revive")) {
                print("player is not dead but pressed Revive");
                RevivePlayer();
            }
        }
    }
    /*
    // Update is called once per frame
    void Update()
    {
        
    }
    */
}
