using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotAnimations : MonoBehaviour
{
    public enum BotStates { Idle,Walking,Pointing,Waving,Talking1,Talking2,Talking3,Hit,Shot,Computer};

    Animator thisAnim;

   

    public GameObject botHead;
    public GameObject lookAt;
    bool isLookingAtPlayer;
    bool isLookingAway;

    private float t = 0;
    private void Awake()
    {
        thisAnim = GetComponent<Animator>();
       
    }
    private void OnAnimatorIK(int layerIndex)
    {
        t += Time.deltaTime;
        if (!isLookingAtPlayer)
        {
            thisAnim.SetLookAtWeight(Mathf.Lerp(1f,0f,t));
          
        }
        else
        {
            thisAnim.SetLookAtWeight(Mathf.Lerp(0f, 1f, t));

        }
        thisAnim.SetLookAtPosition(Camera.main.transform.position);
    }

    private void Update()
    {
        if (LineOfSight() && isInFront()&& !isLookingAtPlayer)
        {
            t = 0;
            isLookingAtPlayer = true;
        }
        else if((!LineOfSight() || !isInFront()) && isLookingAtPlayer)
        {
            t = 0;
            isLookingAtPlayer = false;
        }
      
    }

    bool LineOfSight()
    {
       
        float dist = Vector3.Distance(botHead.transform.position, Camera.main.transform.position);
        Ray ray = new Ray(botHead.transform.position, (Camera.main.transform.position - botHead.transform.position) * dist);
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
        Vector3 directionToTarget = Camera.main.transform.position - botHead.transform.position;
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
