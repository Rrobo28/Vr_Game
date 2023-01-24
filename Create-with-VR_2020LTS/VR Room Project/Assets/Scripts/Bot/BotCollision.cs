using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotCollision : MonoBehaviour
{
    private BotBehaviour behaviour;

    private void Awake()
    {
        behaviour = GetComponent<BotBehaviour>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Object") && !collision.gameObject.GetComponent<GrabHighlight>().inHand && GetComponent<BotBehaviour>().interuptFinished)
        {
            behaviour.interupted = true;
        }
    }
}
