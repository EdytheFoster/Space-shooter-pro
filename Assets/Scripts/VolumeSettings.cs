using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField]
    private AudioMixer _mixer;
    [SerializeField]
    private Slider _backgroundMusicSlider;
    [SerializeField]
    private Slider _explosionsSlider;

    public const string MIXER_MUSIC = "Background Music";
    public const string MIXER_EXPLOSIONS = "Explosions";

    private void Awake()
    {
        _backgroundMusicSlider.onValueChanged.AddListener(SetBackgroundMusic);
        _explosionsSlider.onValueChanged.AddListener(SetExplosions);
    }

    private void Start()
    {
        _backgroundMusicSlider.value = PlayerPrefs.GetFloat(AudioManager.MUSIC_KEY, 1f);
        _explosionsSlider.value = PlayerPrefs.GetFloat(AudioManager.EXPLOSIONS_KEY, 1f);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(AudioManager.MUSIC_KEY, _backgroundMusicSlider.value);
        PlayerPrefs.SetFloat(AudioManager.EXPLOSIONS_KEY, _explosionsSlider.value);
    }

    private void SetBackgroundMusic(float value)

    {
        _mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(value) * 20);
    }

    private void SetExplosions(float value)
    {
        _mixer.SetFloat(MIXER_EXPLOSIONS, Mathf.Log10(value) * 20);
    }
}
