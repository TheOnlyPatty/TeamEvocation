using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{

    public AudioMixer mixer;

    public void SetLevelMusic(float sliderValue)
    {
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
    }

    public void SetLevelSFX(float slidervalue)
    {
        mixer.SetFloat("SFXVol", Mathf.Log10(slidervalue) * 20);
    }
}