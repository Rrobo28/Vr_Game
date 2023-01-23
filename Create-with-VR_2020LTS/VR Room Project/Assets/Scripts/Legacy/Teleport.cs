using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class Teleport : MonoBehaviour
{
    public GameObject rig;

    public Transform teleport;

    GameObject timerStartObject;

    public GameObject startGun;
   public void GunChosen(SelectEnterEventArgs args)
    {
        StartCoroutine(TP());
        
    }

    IEnumerator TP()
    {
        yield return new WaitForSeconds(2);

        rig.transform.position = teleport.position;
        rig.transform.rotation = teleport.rotation;

        Destroy(startGun);
    }
}
