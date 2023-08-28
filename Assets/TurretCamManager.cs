using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretCamManager : MonoBehaviour {

    public static Transform turretCamManager;
    public static Transform canvasMiddleCrosshair;
    public bool isSomos = false;
    public Sprite crosshairRegular;
    public Sprite crosshairSomos;
    // Start is called before the first frame update
    void Start() {
        turretCamManager = transform;
        canvasMiddleCrosshair = transform.Find("CanvasMiddle_Crosshair");
        canvasMiddleCrosshair.gameObject.SetActive(false);
    }
    public static void EnableMiddleCamera(bool somos = false) {
        if(canvasMiddleCrosshair) {
            canvasMiddleCrosshair.gameObject.SetActive(true);
            canvasMiddleCrosshair.GetComponent<CameraCrosshairController>().SetColorToBlue(true);
        }

        if(somos) {
            Transform crosshair = turretCamManager.Find("CanvasMiddle_Crosshair/CrosshairBG/Crosshair");
            crosshair.GetComponent<Image>().sprite = turretCamManager.GetComponent<TurretCamManager>().crosshairSomos;
        } else {
            Transform crosshair = turretCamManager.Find("CanvasMiddle_Crosshair/CrosshairBG/Crosshair");
            crosshair.GetComponent<Image>().sprite = turretCamManager.GetComponent<TurretCamManager>().crosshairRegular;
        }
    }
    public static void BlackifyMiddleCamera() {
        canvasMiddleCrosshair.gameObject.SetActive(true);
        canvasMiddleCrosshair.GetComponent<CameraCrosshairController>().SetColorToBlue(false);
    }
    public static void DisableMiddleCamera() {
        canvasMiddleCrosshair.GetComponent<CameraCrosshairController>().SetColorToBlue(false);
        canvasMiddleCrosshair.gameObject.SetActive(false);
    }


}
