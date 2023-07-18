using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameObject gameManagerObj;
    public static bool playerIsAlive;

    // Start is called before the first frame update
    void Awake()
    {
        gameManagerObj = gameObject;
    }
    public void KillPlayer() {
        print("player must die");
    }
    /*
    // Update is called once per frame
    void Update()
    {
        
    }
    */
}
