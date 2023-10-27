using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

/*
* Author: Alexis Clay Drain
*/
public class DialogueType : MonoBehaviour {

    // reason I'm putting all the story here is so that it's a little easier to localize if I ever decide to.
    [TextArea(3, 5)]
    public string story1 = "";
    [TextArea(3, 5)]
    public string story2 = "";
    [TextArea(3, 5)]
    public string story3 = "";
    [TextArea(3,5)]
    public string story4 = "";

    private string finalText = "";
    private Text myText;

    private void Start() {
        myText = GetComponent<Text>();

        myText.text = "";
        myText.enabled = true; // disabled in the editor and enabled here so that the first frame doesn't show the full text
    }

    public void StartTypewriter(int storyIndex) {

        if(storyIndex == 1) {
            finalText = story1;
        } else if (storyIndex == 2) {
            finalText = story2;
        } else if(storyIndex == 3) {
            finalText = story3;
        } else if (storyIndex == 4) {
            finalText = story4;
        }

        StartCoroutine(Typewriter());
    }

    IEnumerator Typewriter() {

        foreach (char c in finalText) {
            //if (c != ' ' || c != '\n') {
            //    textAudioSource.Play();
            //}
            myText.text += c;
            yield return new WaitForSeconds(.01f);
        }
    }

}
