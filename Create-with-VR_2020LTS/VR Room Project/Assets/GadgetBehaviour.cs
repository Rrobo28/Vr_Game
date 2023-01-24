using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GadgetBehaviour : BotBehaviour
{

    private void Start()
    {
        dialogue = Dialogue();
      

    }
    public override void PlayerHere()
    {
        base.PlayerHere();
        Debug.Log("GADGET SAYS HELLO");
        StartCoroutine(Dialogue());

    }
    IEnumerator Dialogue()
    {
        eyeControl.SetEmotion(Emotion.Type.Netrual);

        animations.SetState(BotAnimations.BotStates.Computer);

        yield return null;
        /*
        for (int i = soundIndex; soundIndex < audio.currentScene.Length; soundIndex++)
        {

            Audio currentAudioPlaying = audio.GetAudio(soundIndex);
            Debug.Log(currentAudioPlaying.clip.name);
            animations.SetState(currentAudioPlaying.state);

            yield return new WaitForSeconds(currentAudioPlaying.animationDelay);

            audio.Play(soundIndex);


            yield return new WaitForSeconds(currentAudioPlaying.clip.length);


        }*/



       


    }
}
