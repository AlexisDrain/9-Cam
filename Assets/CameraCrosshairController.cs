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
    // Start is called before the first frame update
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
        }
    }
}
