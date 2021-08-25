using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioSource[] audioSource;
    public static Dictionary<string, AudioSource> sounds;

    private void Awake()
    {
        audioSource = GetComponents<AudioSource>();
        sounds = new Dictionary<string, AudioSource>();

        sounds.Add("", audioSource[0]);
        sounds.Add("Gun_2", audioSource[1]);
        sounds.Add("Gun_3", audioSource[2]);
        sounds.Add("Gun_4", audioSource[3]);
        sounds.Add("Gun_5", audioSource[4]);
    }
    //  SoundManager.Play("Effect_Getcombine");
    public static void Play(string soundName)
    {
        if (sounds.ContainsKey(soundName))
        {
            sounds[soundName].Play();
        }
    }

    public static void Stop(string soundName)
    {
        if (sounds.ContainsKey(soundName))
        {
            sounds[soundName].Stop();
        }
    }
}
