using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Door : MonoBehaviour
{
    bool hasCollided = false;

    bool leftCollision = false;

    public GameObject door1;
    public GameObject door2;

   
  
    public bool stayOpen;
    private void Update()
    {
        if (hasCollided)
        {
            StartCoroutine(MoveDoorOpen());

            OpenDoor();

        }
        if (leftCollision)
        {
            StartCoroutine(MoveDoorClose());
            CloseDoor();
        }
    }

    void OpenDoor()
    {
        door1.transform.position = Vector3.Lerp(door1.transform.position, new Vector3(door1.transform.position.x, door1.transform.position.y + 1.4f, door1.transform.position.z), Time.deltaTime);
        door2.transform.position = Vector3.Lerp(door2.transform.position, new Vector3(door2.transform.position.x, door2.transform.position.y - 1.4f, door2.transform.position.z), Time.deltaTime);

        foreach(MeshRenderer mesh in GetComponentsInChildren<MeshRenderer>())
        {
            Material[] materials = mesh.materials;

            foreach (Material mat in materials)
            {
                mat.SetColor("_EmissionColor", Color.green); 
            }
            mesh.materials = materials;
        }
    }

    void CloseDoor()
    {
        door1.transform.position = Vector3.Lerp(door1.transform.position, new Vector3(door1.transform.position.x, door1.transform.position.y - 1.4f, door1.transform.position.z), Time.deltaTime);
        door2.transform.position = Vector3.Lerp(door2.transform.position, new Vector3(door2.transform.position.x, door2.transform.position.y + 1.4f, door2.transform.position.z), Time.deltaTime);
    }

    IEnumerator MoveDoorOpen()
    {
       
        yield return new WaitForSeconds(1);
        hasCollided = false;
    }

    IEnumerator MoveDoorClose()
    {

        yield return new WaitForSeconds(1);
        leftCollision = false;
    }

    private void OnTriggerEnter(Collider other)
    {
      
        if (other.gameObject.CompareTag("Bot") || other.gameObject.CompareTag("MainCamera"))
        {
           
            hasCollided = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (stayOpen)
            return;
        if (other.gameObject.CompareTag("Bot") || other.gameObject.CompareTag("MainCamera"))
        {

            leftCollision = true;
        }
    }
}
