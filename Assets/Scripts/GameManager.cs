using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static Transform playerCheckpoint;
    public static Transform checkpointCameraBundle;
    public static GameObject currentLevel;

    public static GameObject gameManagerObj;
    private static Pool pool_LoudAudioSource;
    public static GameObject player;
    public static GameObject worldObj;
    public static GameObject canvasMenu;
    public static GameObject canvasTopRightTutorial;
    public static GameObject canvasCrouchTutorial;

    public static bool playerIsAlive = true;
    public bool cheatMode = true;

    public static LayerMask worldMask;
    public static LayerMask entityMask;
    public static LayerMask triggersMask;

    public static UnityEvent playerRevive = new UnityEvent(); 
    public List<GameObject> levelList;

    void Awake() {
        gameManagerObj = gameObject;
        pool_LoudAudioSource = transform.Find("Pool_LoudAudioSource").GetComponent<Pool>();
        player = GameObject.Find("Player");
        worldObj = GameObject.Find("World");
        canvasMenu = GameObject.Find("CanvasMenu");
        canvasTopRightTutorial = GameObject.Find("CanvasTopRightTutorial");
        canvasTopRightTutorial.SetActive(false);
        canvasCrouchTutorial = GameObject.Find("CanvasCrouchTutorial");
        canvasCrouchTutorial.SetActive(false);

        worldMask = LayerMask.NameToLayer("World");
        entityMask = LayerMask.NameToLayer("Entity");
        triggersMask = LayerMask.NameToLayer("Triggers");


        currentLevel = levelList[0];
        playerCheckpoint = currentLevel.GetComponent<LevelValues>().firstPlayerCheckpoint;
        checkpointCameraBundle = currentLevel.GetComponent<LevelValues>().firstCameraBundle;

        Time.timeScale = 0f;
        //NewGame();
    }
    public static void KillPlayer() {
        print("player must die");
        playerIsAlive = false;
    }
    public static void RevivePlayer() {
        print("Revive Player");
        playerIsAlive = true;
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.GetComponent<Rigidbody>().rotation = playerCheckpoint.rotation;
        player.transform.rotation = playerCheckpoint.rotation;
        player.GetComponent<Rigidbody>().position = playerCheckpoint.position;
        player.transform.position = playerCheckpoint.position;

        GameManager.checkpointCameraBundle.gameObject.SetActive(true);

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

        if(cheatMode == true) {
            if((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                && (Input.GetKeyDown(KeyCode.F3) || Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))) {
                NewGame();
            }
            if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            && (Input.GetKeyDown(KeyCode.F4) || Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))) {
                GameManager.canvasTopRightTutorial.SetActive(false);
            }
        }
    }
    public void NewGame() {
        print("New Game: Spawn intro Level");

        Time.timeScale = 1.0f;

        canvasMenu.SetActive(false);
        canvasTopRightTutorial.SetActive(true);

        for (int i = 0; i < GameManager.worldObj.transform.childCount; i++) {
            Destroy(GameManager.worldObj.transform.GetChild(i).gameObject);
        }

        GameManager.currentLevel = levelList[0];
        GameManager.currentLevel = GameObject.Instantiate(GameManager.currentLevel, new Vector3(0f, 0f, 0f), Quaternion.identity, GameManager.worldObj.transform);

        // start game
        GameManager.playerCheckpoint = GameManager.currentLevel.GetComponent<LevelValues>().firstPlayerCheckpoint;
        GameManager.checkpointCameraBundle = GameManager.currentLevel.GetComponent<LevelValues>().firstCameraBundle;

        GameManager.RevivePlayer();
    }
    /*
    // Update is called once per frame
    void Update()
    {
        
    }
    */
}
