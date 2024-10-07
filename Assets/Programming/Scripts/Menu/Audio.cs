using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Audio : MonoBehaviour
{
    //public AudioSource audioSource;
    //public void ChangeAudio(float volume)
    //{
    //    audioSource.volume = volume;
    //}
    public Text audioDisplay;
    public AudioMixer audioMixer;
    [SerializeField] string audioMixerChannel;
    public void CurrentMixer(string name)
    {
        audioMixerChannel = name;        
    }
    public void ChangeVolume(float volume)
    {
        audioMixer.SetFloat(audioMixerChannel, volume);
        ChangeText(name, volume);
    }
    void ChangeText(string name, float volume)
    {
        audioDisplay.text = $"{Mathf.Clamp01((volume+80)/100):P0}";
    }
    public void GetText(Text UIText)
    {
        audioDisplay = UIText;
    }
}
