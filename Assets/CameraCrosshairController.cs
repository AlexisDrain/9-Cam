using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraCrosshairController : MonoBehaviour
{
    public bool activeState = true;
    public Image crosshairBG;

    public Color activeColor;
    public Color disabledColor = Color.black;


    private void Start() {

    }

    void OnEnable()
    {
        SetColorToBlue(true);
    }

    // Update is called once per frame
    public void SetColorToBlue(bool state)
    {
        activeState = state;
        if(state == true) {
            crosshairBG.color = activeColor;

        } else {
            crosshairBG.color = disabledColor;

            //GameManager.mainCameraMiddle.GetComponent<Camera>().cullingMask = 0;
        }
    }
}
