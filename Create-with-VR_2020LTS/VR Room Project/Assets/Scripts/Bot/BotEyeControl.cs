using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct Emotion
{
    public enum Type { Netrual, Happy, Angry, Dead, Sad }
    public Type type;
    public Vector2 eyePosition;
    public float AnimationBlend;
}

public class BotEyeControl : MonoBehaviour 
{
    public List<Emotion> emotionList = new List<Emotion>();

    public GameObject eyes;

    public Material eyeMaterial;

    private Emotion currentEmotion;

    private void Awake()
    {
        eyeMaterial = eyes.GetComponent<SkinnedMeshRenderer>().material;
    }

    public void SetEmotion(Emotion.Type type)
    {
        foreach (Emotion emotion in emotionList)
        {
            if(emotion.type == type)
            {
                currentEmotion = emotion;
                eyeMaterial.mainTextureOffset = new Vector2(emotion.eyePosition.x,emotion.eyePosition.y);
                return;
            }
        }
    }
    public Emotion GetEmotion()
    {
        return currentEmotion;
    }



}
