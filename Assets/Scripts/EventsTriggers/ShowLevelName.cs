using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowLevelName : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Called by GameManager around line 300
    public void ShowLevel(string levelName)
    {
        if(levelName == "MUSEUM OF HUMANS & OTHER EXTINCT ANIMALS") {
            GetComponent<Text>().fontSize = 200;
        } else {
            GetComponent<Text>().fontSize = 300;
        }

        GetComponent<AudioSource>().PlayWebGL();
        GetComponent<Text>().text = levelName;
        GetComponent<Text>().enabled = true;

        StartCoroutine(HideLevelCountdown());
    }
    private IEnumerator HideLevelCountdown() {
        yield return new WaitForSecondsRealtime(4f);
        GetComponent<Text>().enabled = false;
    }
}
