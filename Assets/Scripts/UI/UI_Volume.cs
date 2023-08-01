using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/*
* Author: Alexis Clay Drain
*/
public class UI_Volume : MonoBehaviour
{
	public AudioMixer audioMixer;
	public Text text_VolumeAmount;
    public Slider slider;
	public bool music = true;

	public void VolumeIncreaseByPoint5() {
        /* OldRange = (OldMax - OldMin)
			NewRange = (NewMax - NewMin)
			NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin */
        slider.value += 0.05f;
        slider.value = Mathf.Clamp01(slider.value);

		UpdateValues();
    }
    public void VolumeDecreaseByPoint5() {
        /* OldRange = (OldMax - OldMin)
			NewRange = (NewMax - NewMin)
			NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin */
        slider.value -= 0.05f;
        slider.value = Mathf.Clamp01(slider.value);

        UpdateValues();
    }
    private void UpdateValues() {
        float oldRange = (1 - 0); // 0 to 1
        float newRange = (0 - -80); // -80 to 0
        float newMixerValue = (slider.value * newRange) / oldRange + (-80);

        text_VolumeAmount.text = slider.value.ToString("0.00");
        if (music) {
            audioMixer.SetFloat("MusicVolume", newMixerValue);
        } else {
            audioMixer.SetFloat("SFXVolume", newMixerValue);
        }
    }

    // Old
	public void SetMusicVolume(float newVolume) {

		audioMixer.SetFloat("MusicVolume", newVolume);
	}
	public void SetSFXVolume(float newVolume) {

		audioMixer.SetFloat("SFXVolume", newVolume);

	}

}
