using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotBehaviour : MonoBehaviour
{
    protected AudioController audio;
    protected BotAnimations animations;
    protected BotMovement movement;
    protected BotEyeControl eyeControl;


    protected IEnumerator dialogue;
    protected IEnumerator interuption;

    protected int soundIndex = 0;

    public bool interupted = false;

    public bool hasBeenShot = false;

    public bool interuptFinished = true;

    private void Awake()
    {
        audio = GetComponent<AudioController>();
        animations = GetComponent<BotAnimations>();
        movement = GetComponent<BotMovement>();
        eyeControl = GetComponent<BotEyeControl>();
    }

    private void Start()
    {
        interuption = Interuption();
    }


    private void Update()
    {
        if (interupted && interuptFinished)
        {
            StartInteruption();
        }
        else if (hasBeenShot && interuptFinished)
        {
            StartShotInteruption();
        }
    }

    public virtual void PlayerHere()
    {
        
    }


   
    public void StartInteruption()
    {
        interuptFinished = false;
        interupted = false;
        StopAllCoroutines();
     
        StartCoroutine(Interuption());

    }
    public void StartShotInteruption()
    {
        interuptFinished = false;
        hasBeenShot = false;
        StopAllCoroutines();
       
        StartCoroutine(ShotInteruption());

    }

    IEnumerator ShotInteruption()
    {
      
        Audio[] prevAudio = audio.currentScene;
        audio.currentScene = audio.shotSounds;

        audio.ResetAudio();
        eyeControl.SetEmotion(Emotion.Type.Angry);

        int randomSound = Random.Range(0, audio.shotSounds.Length);

        animations.SetState(audio.GetAudio(randomSound).state);

        audio.Play(randomSound);

        yield return new WaitForSeconds(6);

        audio.currentScene = prevAudio;


        eyeControl.SetEmotion(Emotion.Type.Netrual);
        ResetIndex();
        StartCoroutine(dialogue);

        interuptFinished = true;

    }


    IEnumerator Interuption()
    {
       
        Audio[] prevAudio = audio.currentScene;
        audio.currentScene = audio.hitSounds;
      
        audio.ResetAudio();
        eyeControl.SetEmotion(Emotion.Type.Sad);

        int randomSound = Random.Range(0, audio.hitSounds.Length);
        audio.Play(randomSound);

        yield return new WaitForSeconds(audio.GetClipLength(randomSound));
        audio.currentScene = prevAudio;
      

        eyeControl.SetEmotion(Emotion.Type.Netrual);
        ResetIndex();
        StartCoroutine(dialogue);

        interuptFinished = true;

    }

    void ResetIndex()
    {
        if (soundIndex <= 0)
        {
            soundIndex = 0;
        }
        else
        {
            soundIndex--;
        }
    }
}
