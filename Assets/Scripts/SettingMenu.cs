using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    // Method to set volume, sliderValue is passed from the slider
    public void SetVolume(float sliderValue)
    {
        // Convert slider value (0.001 to 1) to decibels (-80 dB to 0 dB)
        float dB = Mathf.Log10(sliderValue) * 20;
        audioMixer.SetFloat("volume", dB);

        Debug.Log($"Slider Value: {sliderValue}, Volume in dB: {dB}");
    }
}

