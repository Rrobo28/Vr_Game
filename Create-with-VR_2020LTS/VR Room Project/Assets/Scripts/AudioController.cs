using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct Audio
{
    public AudioClip clip;
    public BotAnimations.BotStates state;
    public float animationDelay;
}

public class AudioController : MonoBehaviour
{
    public Audio[] introScene;

    public Audio[] hitSounds;

    public Audio[] shotSounds;
    public Audio[] currentScene;

    public AudioSource thisSource;

  


    public float GetClipLength(int index)
    {
        return currentScene[index].clip.length;
    }
    public Audio GetAudio(int index)
    {
        return currentScene[index];
    }
    public void PauseAudio()
    {
        thisSource.Pause();
    }
    public void ResetAudio()
    {
        thisSource.Stop();
    }

    public void Play(int index)
    {
        thisSource.clip = currentScene[index].clip;
       
        thisSource.Play();

    }
}
