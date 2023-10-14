using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    AudioMixer mixer;
    public static AudioManager instance;
    public const string MUSIC_KEY = "backgroundMusicVolume";
    public const string EXPLOSIONS_KEY = "explosionsVolume";


    void LoadVolume()
    {
        float backgroundmusicVolume = PlayerPrefs.GetFloat(MUSIC_KEY, 1f);
        float explosionsVolume = PlayerPrefs.GetFloat(EXPLOSIONS_KEY, 1f);

        mixer.SetFloat(VolumeSettings.MIXER_MUSIC, Mathf.Log10(backgroundmusicVolume) * 20);
        mixer.SetFloat(VolumeSettings.MIXER_EXPLOSIONS, Mathf.Log10(explosionsVolume) * 20);
    }

}
