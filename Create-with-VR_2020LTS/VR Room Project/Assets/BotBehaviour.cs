using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotBehaviour : MonoBehaviour
{
    private AudioController audio;
    private BotAnimations animations;
    private BotMovement movement;
    private BotEyeControl eyeControl;

    
    IEnumerator intro;
    IEnumerator interuption;
    int soundIndex = 0;

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
        intro = Intro();
        interuption = Interuption();
        StartCoroutine(intro);
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

    IEnumerator Intro()
    {
        eyeControl.SetEmotion(Emotion.Type.Happy);
        audio.currentScene = audio.introScene;
       
        movement.WalkTo(BotMovement.WalkPoints.StartingRoom);
       
        while (!movement.IsStopped())
        {
            yield return null;
        }
       
        eyeControl.SetEmotion(Emotion.Type.Netrual);


      
        for (int i = soundIndex; soundIndex < audio.currentScene.Length; soundIndex++)
        { 

            Audio currentAudioPlaying = audio.GetAudio(soundIndex);
            Debug.Log(currentAudioPlaying.clip.name);
            animations.SetState(currentAudioPlaying.state);

            yield return new WaitForSeconds(currentAudioPlaying.animationDelay);

            audio.Play(soundIndex);
           

            yield return new WaitForSeconds(currentAudioPlaying.clip.length);

            
        }
     

       
        //movement.WalkTo(BotMovement.WalkPoints.MainHall);
       

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
        StartCoroutine(intro);

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
        StartCoroutine(intro);

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
