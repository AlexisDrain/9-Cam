using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameObject gameManagerObj;
    public static GameObject player;
    public static bool playerIsAlive;

    // Start is called before the first frame update
    void Awake()
    {
        gameManagerObj = gameObject;
        player = GameObject.Find("Player");
    }
    public void KillPlayer() {
        print("player must die");
    }
    public static void SpawnLoudAudio(AudioClip spawnAudio) {
        print("GameManager TODO: spawn sfx");
    }
    /*
    // Update is called once per frame
    void Update()
    {
        
    }
    */
}
