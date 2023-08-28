using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WorldButtonController : MonoBehaviour
{
    public AudioClip deactivateAudioClip;
    public AudioClip activateAudioClip;
    public AudioSource audioSource;
    public Animator myAnimator;

    [Header("Read only")]
    public bool isActive = false;
    void Start()
    {
        GameManager.playerRevive.AddListener(ResetButtonVisual);
    }

    public void ActivateButtonVisual() {
        if(isActive == false) {
            myAnimator.SetTrigger("On");
            audioSource.PlayWebGL(activateAudioClip);
            isActive = true;
        }
    }

    void ResetButtonVisual()
    {
        if(isActive == true) {
            myAnimator.SetTrigger("Off");
            audioSource.PlayWebGL(deactivateAudioClip);
            isActive = false;
        }
    }
}
