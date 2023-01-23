using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class GrabbableObject : MonoBehaviour
{
    public void SelectEntered(SelectEnterEventArgs e)
    {
        GetComponent<Outline>().enabled = true;
    }
   public void SelectExited(SelectEnterEventArgs e)
    {
        GetComponent<Outline>().enabled = false;

   }
}
