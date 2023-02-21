using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
public class Fader : MonoBehaviour
{
    public Volume pp;
    private Vignette vignette;
    public bool Done;
    float num = 0;

    private void Awake()
    {
       
    }

    public bool StartFadeOut()
    {
        Done = false;
        pp.profile.TryGet(out vignette);
       
        num += Time.deltaTime;
        Debug.Log(num);
        vignette.intensity.value = num;
        while (num < 1f)
        {
            return false;
        }
        num = 1;
        return true;

    }
    public bool StartFadeIn()
    {
        Done = false;
        pp.profile.TryGet(out vignette);

        num -= Time.deltaTime;
        Debug.Log(num);
        vignette.intensity.value = num;
        while (num >= 0f)
        {
            return false;
        }
        num = 0;
        return true;

    }



}
