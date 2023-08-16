using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCamManager : MonoBehaviour
{

    public static Transform canvasMiddleCrosshair;
    // Start is called before the first frame update
    void Start()
    {
        canvasMiddleCrosshair = transform.Find("CanvasMiddle_Crosshair");
        canvasMiddleCrosshair.gameObject.SetActive(false);
    }
    public static void EnableMiddleCamera() {
        canvasMiddleCrosshair.gameObject.SetActive(true);
        canvasMiddleCrosshair.GetComponent<CameraCrosshairController>().SetColorToBlue(true);
    }
    public static void BlackifyMiddleCamera() {
        canvasMiddleCrosshair.gameObject.SetActive(true);
        canvasMiddleCrosshair.GetComponent<CameraCrosshairController>().SetColorToBlue(false);
    }
    public static void DisableMiddleCamera() {
        canvasMiddleCrosshair.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
