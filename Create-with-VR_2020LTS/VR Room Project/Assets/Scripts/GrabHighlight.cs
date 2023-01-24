using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class GrabHighlight : MonoBehaviour
{
    private Outline thisOutline;
    [HideInInspector]
    public bool inHand = false;

    private void Awake()
    {
        thisOutline = GetComponent<Outline>();
    }

    public void EnableHighlight(HoverEnterEventArgs args)
    {
        
        if(args.interactable.gameObject.TryGetComponent(out GrabHighlight highlight) && !highlight.inHand)
        thisOutline.enabled = true;
    }
    public void DisableHighlight(HoverExitEventArgs args)
    {
        thisOutline.enabled = false;
    }
    public void DisableHighlight(SelectEnterEventArgs args)
    {
        thisOutline.enabled = false;
        inHand = true;
    }
    public void Dropped(SelectExitEventArgs args)
    {
        inHand = false;
        if (args.interactable.gameObject.TryGetComponent(out ObjectControls controls) && controls.objectHeld)
        {
            controls.objectHeld = false;
        }
       
    }



}
