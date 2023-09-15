using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

/*
* Author: Alexis Clay Drain
*/
public class DialogueType : MonoBehaviour
{
    private string finalText = "";
    private Text myText;

    private void Start() {
        myText = GetComponent<Text>();

        finalText = GetComponent<Text>().text;
        myText.text = "";
        myText.enabled = true; // disabled in the editor and enabled here so that the first frame doesn't show the full text
    }

    public void StartTypewriter() {
        StartCoroutine(Typewriter());
    }

    IEnumerator Typewriter() {
        yield return new WaitForSeconds(2f);
        foreach (char c in finalText) {
            //if (c != ' ' || c != '\n') {
            //    textAudioSource.Play();
            //}
            myText.text += c;
            yield return new WaitForSeconds(.01f);
        }
    }

}
