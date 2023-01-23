using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAnimations : MonoBehaviour
{
    public GameObject trigger;
    public GameObject barrel;

    private ObjectControls controls;

    private void Awake()
    {
        controls = GetComponent<ObjectControls>();
    }

  

    private void Update()
    {
        if (controls.objectHeld)
        {
            TriggerAnimation();
        }
    }
    public void TriggerAnimation()
    {
        trigger.transform.localRotation = Quaternion.Euler(trigger.transform.localRotation.x, trigger.transform.localRotation.y, controls.GetTriggerValue() * 20);
    }
    public void BarrelAnimation()
    {   
        barrel.transform.Rotate(new Vector3(20, 0, 0), Space.Self);
    }
}
