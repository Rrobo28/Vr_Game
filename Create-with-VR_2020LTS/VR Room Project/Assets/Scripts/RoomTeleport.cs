using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;
public class RoomTeleport : MonoBehaviour
{
    public enum Room { Training};
    public Room room;

    private void Start()
    {
       //StartCoroutine(SwapToRoom());
    }

    public void ChangeScene(SelectExitEventArgs args)
    {
        StartCoroutine(SwapToRoom());
    }

    IEnumerator SwapToRoom()
    {
        yield return new WaitForSeconds(2);
        GameObject.Find("Scene").GetComponent<SceneLoader>().LoadNewScene("Training");
    }
}
