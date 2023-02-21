using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Class for the melee enemy
public class AIMelee : AIAction
{
    bool isMoving = false;

    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        GetComponent<Animator>().SetFloat("Blend", 1);
    }

    private void Update()
    {
        if (!GetComponent<AI>().isReady)
        {
            return;
        }

        if (!isMoving)
        {
            MoveToPlayer();

        }
        DistCheck();
    }

    void MoveToPlayer()
    {
        agent.SetDestination(Camera.main.transform.position);
        isMoving = true;
    }

    void DistCheck()
    {
        Debug.Log(Vector3.Distance(transform.position, Camera.main.transform.position));
        if (isMoving && !GetComponent<AI>().isShot)
        {
            if (Vector3.Distance(transform.position, Camera.main.transform.position) <= 2f)
            {
                GameObject.Find("GameMode").GetComponent<WaveManager>().ResetWave();
            }
        }
    }
}
