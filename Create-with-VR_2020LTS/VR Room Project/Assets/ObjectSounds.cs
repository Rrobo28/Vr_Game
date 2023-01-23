using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSounds : MonoBehaviour
{
    private AudioSource thisSource;
    public AudioClip sound;

    private void Awake()
    {
        thisSource = GetComponent<AudioSource>();
        thisSource.playOnAwake = false;
        thisSource.clip = sound;
    }

    public void PlaySound()
    {
        thisSource.Play();
    }
}
