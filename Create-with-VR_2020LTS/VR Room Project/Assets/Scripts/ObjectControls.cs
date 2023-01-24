using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
public class ObjectControls : MonoBehaviour
{
    List<UnityEngine.XR.InputDevice> gameControllers = new List<UnityEngine.XR.InputDevice>();
    public InputDevice device;

    public bool objectHeld;
    public void SelectEnter(SelectEnterEventArgs args)
    {
        objectHeld = true;
        if (args.interactor.gameObject.name.Contains("Left"))
        {
            UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.LeftHand, gameControllers);
        }
        else if (args.interactor.gameObject.name.Contains("Right"))
        {
            UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.RightHand, gameControllers);
        }

        device = gameControllers[0];
    }
    public void SelectExit(SelectExitEventArgs args)
    {
        objectHeld = false;
    }

    public float GetTriggerValue()
    {
        if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.trigger, out float triggerValue))
        {
            return triggerValue;
        }
        else
        {
            return 0;
        }
    }
    public void SetHapticOfDevice(float amplitude,float duration)
    {
        device.SendHapticImpulse(0u, amplitude, duration);
    }
}
