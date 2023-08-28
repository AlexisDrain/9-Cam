using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelValues : MonoBehaviour
{
    public Transform firstCameraBundle;
    public Transform firstPlayerCheckpoint;
    public string levelName;
    public bool isTurret = false;
    [Header("Somos only")]
    public bool smoothCamBlends = false;
    public bool isSomos = false;

    void Start()
    {
        
    }

}
