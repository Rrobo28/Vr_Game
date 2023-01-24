using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JayBehaviour : BotBehaviour
{

    private void Start()
    {
        dialogue = Dialogue();
        
        StartCoroutine(dialogue);
    }
    IEnumerator Dialogue()
    {
        eyeControl.SetEmotion(Emotion.Type.Happy);
        audio.currentScene = audio.jayVoice;

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
}
