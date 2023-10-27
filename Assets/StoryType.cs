using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryType : MonoBehaviour
{

    // reason I'm putting all the story here is so that it's a little easier to localize if I ever decide to.
    [TextArea(3, 5)]
    public string story1 = "";
    [TextArea(3, 5)]
    public string story2 = "";
    [TextArea(3, 5)]
    public string story3 = "";
    [TextArea(3, 5)]
    public string story4 = "";
    [TextArea(3, 5)]
    public string story5 = "";
    [TextArea(3, 5)]
    public string story6 = "";
    [TextArea(3, 5)]
    public string story7 = "";

    private string finalText = "";
    public Text myText;
    private Canvas myCanvas;

    private void Start() {
        myCanvas = GetComponent<Canvas>();
        myCanvas.enabled = false;

        myText.text = "";
        myText.enabled = true; // disabled in the editor and enabled here so that the first frame doesn't show the full text

        GameManager.playerRevive.AddListener(DisableCanvas);
    }

    public void StartTypewriter(int storyIndex) {

        if (storyIndex == 1) {
            finalText = story1;
        } else if (storyIndex == 2) {
            finalText = story2;
        } else if (storyIndex == 3) {
            finalText = story3;
        } else if (storyIndex == 4) {
            finalText = story4;
        } else if (storyIndex == 5) {
            finalText = story5;
        } else if (storyIndex == 6) {
            finalText = story6;
        } else if (storyIndex == 7) {
            finalText = story7;
        }

        myText.text = "";
        StopCoroutine("Typewriter");
        StartCoroutine("Typewriter");
    }
    public void DisableCanvas() {
        StopCoroutine("Typewriter");
        myText.text = "";
        myCanvas.enabled = false;
    }
    IEnumerator Typewriter() {
        myText.text = "";
        myCanvas.enabled = true;
        foreach (char c in finalText) {
            //if (c != ' ' || c != '\n') {
            //    textAudioSource.Play();
            //}
            myText.text += c;
            yield return new WaitForSeconds(.01f);
        }
        yield return new WaitForSeconds(15f);
        myCanvas.enabled = false;
    }

}
