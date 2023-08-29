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

    // Update is called once per frame
    public void ShowLevel(string levelName)
    {
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
