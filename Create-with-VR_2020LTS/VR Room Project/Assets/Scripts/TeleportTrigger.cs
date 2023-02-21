using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class TeleportTrigger : MonoBehaviour
{
    public BotBehaviour botBehaviour;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MainCamera"))
        {
            botBehaviour.PlayerHere();
        }
    }

 
}
  
