using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Cinemachine;

public class GameManager : MonoBehaviour
{

    public enum CameraTransform {
        None,
        BottomLeft_MainCam,
        Bottom_MainCam,
        BottomRight_MainCam,
        MiddleLeft_MainCam,
        Middle_MainCam,
        MiddleRight_MainCam,
        TopLeft_MainCam,
        Top_MainCam,
        TopRight_MainCam
    }

    public static Transform playerCheckpoint;
    public static Transform checkpointCameraBundle;
    public static GameObject currentLevel;
    public static int currentLevelInt;

    public static GameObject gameManagerObj;
    private static Pool pool_LoudAudioSource;
    public static Pool pool_Bullets;
    public static Pool pool_Gibs;

    public static GameObject player;
    public static GameObject worldObj;
    public static GameObject canvasMenu;
    public static GameObject canvasTopRightTutorial;
    public static GameObject canvasCrouchTutorial;
    public static GameObject canvasJumpTutorial;
    
    //public static GameObject canvasCrosshair;
    public static TurretCamManager turretCamManager;
    public static GameObject canvasScreenTransition;
    public static GameObject canvasDeath;
    public static GameObject canvasStory;
    public static GameObject canvasLevelName;

    public static Transform mainCameras;
    public static Transform mainCameraBottomLeft;
    public static Transform mainCameraBottom;
    public static Transform mainCameraBottomRight;
    public static Transform mainCameraMiddleLeft;
    public static Transform mainCameraMiddle;
    public static Transform mainCameraMiddleRight;
    public static Transform mainCameraTopLeft;
    public static Transform mainCameraTop;
    public static Transform mainCameraTopRight;

    public static bool playerIsAlive = true;
    public static bool gameIsPaused = true;
    public static bool gameHasBeenStartedOnce = false;
    public bool cheatMode = true;

    public static LayerMask worldMask;
    public static LayerMask entityMask;
    public static LayerMask triggersMask;

    public static UnityEvent playerRevive = new UnityEvent(); 
    public List<GameObject> levelList;

    void Awake() {
        gameManagerObj = gameObject;
        pool_LoudAudioSource = transform.Find("Pool_LoudAudioSource").GetComponent<Pool>();
        pool_Bullets = transform.Find("Pool_Bullets").GetComponent<Pool>();
        pool_Gibs = transform.Find("Pool_Gibs").GetComponent<Pool>();

        player = GameObject.Find("Player");
        worldObj = GameObject.Find("World");
        canvasMenu = GameObject.Find("CanvasMenu");
        canvasTopRightTutorial = GameObject.Find("CanvasTopRightTutorial");
        canvasTopRightTutorial.SetActive(false);
        canvasCrouchTutorial = GameObject.Find("CanvasCrouchTutorial");
        canvasCrouchTutorial.SetActive(false);
        canvasJumpTutorial = GameObject.Find("CanvasJumpTutorial");
        canvasJumpTutorial.SetActive(false);
        turretCamManager = GameObject.Find("TurretCamManager").GetComponent<TurretCamManager>();
        //canvasCrosshair = GameObject.Find("TurretCamManager/CanvasCrosshair");
        //canvasCrosshair.SetActive(false);
        canvasScreenTransition = GameObject.Find("CanvasScreenTransition");
        canvasDeath = GameObject.Find("CanvasDeath");
        canvasDeath.SetActive(false);
        canvasStory = GameObject.Find("CanvasStory");
        canvasStory.SetActive(false);
        canvasLevelName = GameObject.Find("CanvasLevelName/BigLevelNameText");
        canvasLevelName.GetComponent<Text>().enabled = false;

        mainCameras = GameObject.Find("MainCameras").transform;
        mainCameraBottomLeft = GameObject.Find("MainCameras/1-BottomLeft-MainCam").transform;
        mainCameraBottom = GameObject.Find("MainCameras/2-Bottom-MainCam").transform;
        mainCameraBottomRight = GameObject.Find("MainCameras/3-BottomRight-MainCam").transform;
        mainCameraMiddleLeft = GameObject.Find("MainCameras/4-MiddleLeft-MainCam").transform;
        mainCameraMiddle = GameObject.Find("MainCameras/5-Middle-MainCam").transform;
        mainCameraMiddleRight = GameObject.Find("MainCameras/6-MiddleRight-MainCam").transform;
        mainCameraTopLeft = GameObject.Find("MainCameras/7-TopLeft-MainCam").transform;
        mainCameraTop = GameObject.Find("MainCameras/8-Top-MainCam").transform;
        mainCameraTopRight = GameObject.Find("MainCameras/9-TopRight-MainCam").transform;

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
        playerIsAlive = false;
        GameManager.canvasDeath.SetActive(true);
        GameManager.player.GetComponent<PlayerController>().graphicGirl.SetActive(false);
        GameManager.player.GetComponent<PlayerController>().shadow.SetActive(false);

        GameObject gibs = GameManager.pool_Gibs.Spawn(GameManager.player.transform.position);
        gibs.transform.rotation = GameManager.player.transform.rotation;
    }
    public static void RevivePlayer() {
        playerIsAlive = true;
        GameManager.canvasDeath.SetActive(false);
        GameManager.player.GetComponent<PlayerController>().graphicGirl.SetActive(true);
        GameManager.player.GetComponent<PlayerController>().graphicGirl.GetComponent<Animator>().Rebind();
        player.GetComponent<PlayerEnemyCollision>().health = 3;
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

        if (Input.GetButtonDown("Pause")) {
            if (gameIsPaused && gameHasBeenStartedOnce == true) {
                gameIsPaused = false;
                canvasMenu.SetActive(false);
                Time.timeScale = 1f;
            } else {
                gameIsPaused = true;
                canvasMenu.SetActive(true);
                Time.timeScale = 0f;
            }
        }

        if (playerIsAlive == false) {
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
            // one level back
            if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            && (Input.GetKeyDown(KeyCode.F1) || Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))) {
                currentLevelInt -= 1;
                GameManager.gameManagerObj.GetComponent<GameManager>().SetNewLevel(currentLevelInt);
            }
            // one level forward
            if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            && (Input.GetKeyDown(KeyCode.F2) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))) {
                currentLevelInt += 1;
                GameManager.gameManagerObj.GetComponent<GameManager>().SetNewLevel(currentLevelInt);
            }
            // new game
            if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                && (Input.GetKeyDown(KeyCode.F3) || Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))) {
                //NewGame();
            }
            // remove tutorial message
            if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            && (Input.GetKeyDown(KeyCode.F4) || Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))) {
                GameManager.canvasTopRightTutorial.SetActive(false);
            }
        }
    }
    // level changes starts with this function: SetNewLevel. It will call the other functions.
    public void SetNewLevel(int level) {
        currentLevelInt = level;
        StartCoroutine(ScreenTransition());
    }
    public IEnumerator ScreenTransition() {
        canvasScreenTransition.GetComponent<Animator>().SetTrigger("FadeInThenOut");
        yield return new WaitForSecondsRealtime(0.5f);
        SwitchLevel(currentLevelInt);
    }
    public void SwitchLevel(int level) {
        Time.timeScale = 1.0f;
        canvasMenu.SetActive(false);
        gameIsPaused = false;
        gameHasBeenStartedOnce = true;

        GameManager.pool_Bullets.DeactivateAllMembers();
        //GameManager.pool_LoudAudioSource.DeactivateAllMembers(); // this stops sounds from playing through level transitions
        GameManager.pool_Gibs.DeactivateAllMembers();

        for (int i = 0; i < GameManager.worldObj.transform.childCount; i++) {
            Destroy(GameManager.worldObj.transform.GetChild(i).gameObject);
        }

        GameManager.currentLevel = GameManager.gameManagerObj.GetComponent<GameManager>().levelList[level];
        GameManager.currentLevel = GameObject.Instantiate(GameManager.currentLevel, new Vector3(0f, 0f, 0f), Quaternion.identity, GameManager.worldObj.transform);


        // start game
        GameManager.playerCheckpoint = GameManager.currentLevel.GetComponent<LevelValues>().firstPlayerCheckpoint;
        GameManager.checkpointCameraBundle = GameManager.currentLevel.GetComponent<LevelValues>().firstCameraBundle;
        if(GameManager.currentLevel.GetComponent<LevelValues>().levelName != "") {
            canvasLevelName.GetComponent<ShowLevelName>().ShowLevel(GameManager.currentLevel.GetComponent<LevelValues>().levelName);
        }
        if (GameManager.currentLevel.GetComponent<LevelValues>().isTurret) {
            if (GameManager.currentLevel.GetComponent<LevelValues>().isSomos) {
                TurretCamManager.EnableMiddleCamera(true);
            } else {
                TurretCamManager.EnableMiddleCamera();
            }
        } else {
            TurretCamManager.DisableMiddleCamera();
        }
        // custom camera blend for Somos
        if (GameManager.currentLevel.GetComponent<LevelValues>().smoothCamBlends) {
            foreach (Transform child in mainCameras) {
                child.GetComponent<CinemachineBrain>().m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.EaseInOut;
            }
        } else {
            foreach (Transform child in mainCameras) {
                child.GetComponent<CinemachineBrain>().m_DefaultBlend.m_Style = CinemachineBlendDefinition.Style.Cut;
            }
        }

        if (GameManager.currentLevel.GetComponent<LevelValues>().story1 == true) {
            GameManager.canvasStory.SetActive(true);
            GameManager.canvasStory.transform.Find("Background/FullScreenText").GetComponent<DialogueType>().StartTypewriter();
        } else {
            GameManager.canvasStory.SetActive(false);
        }

        GameManager.canvasTopRightTutorial.SetActive(false);
        GameManager.canvasCrouchTutorial.SetActive(false);
        GameManager.canvasJumpTutorial.SetActive(false);

        GameManager.RevivePlayer();
    }
    public void NewGame() {
        print("New Game: Spawn intro Level");

        gameHasBeenStartedOnce = true;

        SetNewLevel(0);
    }
    public void ResumeGame() {
        Time.timeScale = 1.0f;
        canvasMenu.SetActive(false);
        gameIsPaused = false;
    }
    /*
    // Update is called once per frame
    void Update()
    {
        
    }
    */
}
