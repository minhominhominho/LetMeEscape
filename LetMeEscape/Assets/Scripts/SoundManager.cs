using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public enum SFXType { Reflection, PlayerDeath, EnemySpawning, EnenmyDeath, BoostItem, BatteryItem, WallItem, GameClear, WallCreating }
public enum UISoundType { StageSelection, Credit, MenuSelect }

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private AudioSource BGMSpeaker;
    [SerializeField] private AudioSource SFXSpeaker;
    [SerializeField] private AudioMixer masterMixer;
    [SerializeField] private Slider bgmAudioSlider;
    [SerializeField] private Slider sfxAudioSlider;
    private int bgmIndex;

    [SerializeField] private List<AudioClip> bgmAudioClip;
    [SerializeField] private List<AudioClip> sfxAudioClip;


    void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
        BGMSpeaker.loop = true;
        SFXSpeaker.loop = false;

        PlayBGM();
    }

    public void PlayBGM()
    {
        BGMSpeaker.clip = bgmAudioClip[bgmIndex++ % bgmAudioClip.Count];
        BGMSpeaker.Play();
    }

    public void PlaySFX(SFXType type)
    {
        SFXSpeaker.PlayOneShot(sfxAudioClip[(int)type]);
    }

    public void StopAllAudio()
    {
        BGMSpeaker.Stop();
        SFXSpeaker.Stop();
    }

    public void BGMAudioControl()
    {
        float sound = bgmAudioSlider.value;

        if (sound == -40f) masterMixer.SetFloat("BGM", -80);
        else masterMixer.SetFloat("BGM", sound * 30 / 40);
    }

    public void SFXAudioControl()
    {
        float sound = sfxAudioSlider.value;

        if (sound == -40f) masterMixer.SetFloat("SFX", -80);
        else masterMixer.SetFloat("SFX", sound * 30 / 40);
    }

    public void ToggleAudioVolume()
    {
        AudioListener.volume = AudioListener.volume == 0 ? 1 : 0;
    }
}
