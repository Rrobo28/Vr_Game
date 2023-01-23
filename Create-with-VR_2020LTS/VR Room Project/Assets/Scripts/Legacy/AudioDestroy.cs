using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDestroy : MonoBehaviour
{
    float time;
    void Start()
    {
        time = GetComponent<AudioSource>().clip.length;
        StartCoroutine(StartTimer());

    }

    IEnumerator StartTimer()
    {
        yield return  new WaitForSeconds(time);

        Destroy(this.gameObject);
    }
    
}
