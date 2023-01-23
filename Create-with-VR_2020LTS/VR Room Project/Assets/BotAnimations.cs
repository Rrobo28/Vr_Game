using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotAnimations : MonoBehaviour
{
    public enum BotStates { Idle,Walking,Pointing,Waving,Talking1,Talking2,Hit,Shot};

    Animator thisAnim;

    public GameObject playerHead;

    public GameObject botHead;
    public GameObject lookAt;
    bool isLookingAtPlayer;
    bool isLookingAway;
    private void Awake()
    {
        thisAnim = GetComponent<Animator>();
    }
    private void OnAnimatorIK(int layerIndex)
    {
        if (!isLookingAtPlayer)
        {
           
            thisAnim.SetLookAtPosition(Vector3.zero);
            
            return;

        }
        
        thisAnim.SetLookAtWeight(1);
        thisAnim.SetLookAtPosition(playerHead.transform.position);
    }

    private void Update()
    {
        if (LineOfSight() && isInFront()&& !isLookingAtPlayer && isLookingAway)
        {
          
            isLookingAtPlayer = true;
            isLookingAway = false;
        }
        else if (!isLookingAway && isLookingAtPlayer)
        {
          
            isLookingAtPlayer = false;
            isLookingAway = true;
           
        }
    }

    bool LineOfSight()
    {
        float dist = Vector3.Distance(botHead.transform.position, playerHead.transform.position);
        Ray ray = new Ray(botHead.transform.position, (playerHead.transform.position - botHead.transform.position) * dist);
        RaycastHit hit;

      
        if (Physics.Raycast(ray, out hit))
        {
           
            if (hit.collider.gameObject.CompareTag("MainCamera"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    bool isInFront()
    {
        Vector3 directionToTarget = playerHead.transform.position - botHead.transform.position;
        float angle = Vector3.Angle(botHead.transform.forward, directionToTarget);
        
        if (Mathf.Abs(angle) < 45)
            return true;
        else
        {
            return false;
        }
    }
    public void SetState(BotStates state)
    {
        Debug.Log(state.ToString());
        thisAnim.SetTrigger(state.ToString());
    }

    public float GetAnimationLength()
    {
        return thisAnim.GetCurrentAnimatorClipInfo(0).Length;
    }
}
