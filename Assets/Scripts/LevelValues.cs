using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelValues : MonoBehaviour
{
    public Transform firstCameraBundle;
    public Transform firstPlayerCheckpoint;
    [TextArea(2,2)]
    public string levelName;
    public bool isTurret = false;
    [Header("Somos only")]
    public bool smoothCamBlends = false;
    public bool isSomos = false;
    [Header("Repeat only - Keep enabled for other levels")]
    public bool enableGibs = true;

    [Header("Ending only")]
    public bool fpsCamera = false;


    void Start()
    {
        
    }

}
