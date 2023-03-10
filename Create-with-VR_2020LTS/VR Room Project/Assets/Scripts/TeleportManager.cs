using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class TeleportManager : MonoBehaviour
{
    public UnityEvent onTeleportActive;
    public UnityEvent onTeleportCancel;

    public InputActionReference teleportActiveRef;

    public GameObject baseController;
    public GameObject teleportControler;

    private void Awake()
    {
        teleportActiveRef.action.performed += TeleportActive;
        teleportActiveRef.action.canceled += TeleportCancel;
    }

    void TeleportActive(InputAction.CallbackContext contect)
    {
        if(this!=null)
        onTeleportActive.Invoke();
    }

    IEnumerator Deactivate()
    {
        yield return null;
        if (this != null)
            onTeleportCancel.Invoke();
     
    }
    void TeleportCancel(InputAction.CallbackContext contect)
    {
        if (this != null)
            StartCoroutine(Deactivate());
    }
}
