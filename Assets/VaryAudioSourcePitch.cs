using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaryAudioSourcePitch : MonoBehaviour
{
    public Vector2 pitchRange = new Vector2(0.9f, 1.1f);
    void Awake()
    {
        GetComponent<AudioSource>().pitch = Random.Range(pitchRange.x, pitchRange.y);
        GetComponent<AudioSource>().PlayWebGL();
    }

    // Update is called once per frame
    void OnEnable() {
        GetComponent<AudioSource>().pitch = Random.Range(pitchRange.x, pitchRange.y);
        GetComponent<AudioSource>().PlayWebGL();
    }
}
