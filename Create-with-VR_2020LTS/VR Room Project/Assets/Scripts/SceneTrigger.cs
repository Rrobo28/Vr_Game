using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("MainCamera"))
        GameObject.Find("Scene").GetComponent<SceneLoader>().LoadNewScene("Training");
    }
}
