using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public struct WalkPoint
{
    public BotMovement.WalkPoints name; 
    public Transform[] path;
}

public class BotMovement : MonoBehaviour
{

    public enum WalkPoints { StartingRoom,MilitaryTraining,EngernerringRoom, MainHall , OutsideRoom1, OutsideRoom2 };

    [Header("Points")]
    
    public List<WalkPoint> pointsList = new List<WalkPoint>();

    private Vector3 currentWalkPoint;

    private NavMeshAgent thisAgent;

    
    private bool hasStopped = false;
    

    IEnumerator moveTo;

    private void Awake()
    {
        
        thisAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        
    }

    public void WalkTo(WalkPoints point)
    {
        moveTo = MoveTo(point);
        StartCoroutine(moveTo);
    }

    public IEnumerator MoveTo(WalkPoints point)
    {
        hasStopped = false;
        thisAgent.isStopped = false;
        foreach (WalkPoint points in pointsList)
        {
            if (points.name == point)
            {
                for (int i =0;i<points.path.Length;i++)
                {
                   
                    thisAgent.SetDestination(points.path[i].position);
                    currentWalkPoint = points.path[i].position;
                    GetComponent<BotAnimations>().SetState(BotAnimations.BotStates.Walking);

                    while (Vector3.Distance(transform.position, currentWalkPoint) > 0.5f)
                    {
                        yield return null;
                    }
                    if (i == points.path.Length - 1)
                    {
                        thisAgent.isStopped = true;
                        hasStopped = true;
                        GetComponent<BotAnimations>().SetState(BotAnimations.BotStates.Idle);
                    }
                }
            }
        }
    }

    public bool IsStopped()
    {
        return hasStopped;
    }
}
