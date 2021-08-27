using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioSource[] audioSource;
    public static Dictionary<string, AudioSource> sounds;
    public static Dictionary<string, AudioSource> bgms;

    private void Awake()
    {
        audioSource = GetComponents<AudioSource>();
        sounds = new Dictionary<string, AudioSource>();
        bgms = new Dictionary<string, AudioSource>();

        sounds.Add("Attack_P", audioSource[0]);

        sounds.Add("Roll", audioSource[1]);
        sounds.Add("FootStep1", audioSource[2]);
        sounds.Add("FootStep2", audioSource[3]);
        sounds.Add("FootStep3", audioSource[4]);
        sounds.Add("FootStep4", audioSource[5]);

        sounds.Add("Coin1", audioSource[6]);
        sounds.Add("Coin2", audioSource[7]);
        sounds.Add("Coin3", audioSource[8]);

        sounds.Add("BoxOpen", audioSource[9]);

        sounds.Add("Error", audioSource[10]);

        sounds.Add("GetItem", audioSource[11]);

        sounds.Add("Tel", audioSource[12]);

        sounds.Add("Destroy", audioSource[13]);

        sounds.Add("Explosion", audioSource[14]);

        sounds.Add("Heal", audioSource[15]);
        sounds.Add("Mistery", audioSource[16]);
        sounds.Add("Devil", audioSource[17]);

        bgms.Add("MainMenu", audioSource[18]);
        bgms.Add("WaitingRoom", audioSource[19]);
        bgms.Add("FirstMap", audioSource[20]);
        bgms.Add("SecondMap", audioSource[21]);
        bgms.Add("BossMap", audioSource[22]);

        sounds.Add("Door", audioSource[23]);
    }
    //  SoundManager.Play("Effect_Getcombine");

    public static void PlayBGM(string BGMName)
    {
        if (bgms[BGMName].isPlaying)
            return;

        bgms["MainMenu"].Stop();
        bgms["WaitingRoom"].Stop();
        bgms["FirstMap"].Stop();
        bgms["SecondMap"].Stop();
        bgms["BossMap"].Stop();

        bgms[BGMName].Play();
    }
    
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
