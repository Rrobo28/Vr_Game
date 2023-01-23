using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class TableFade : MonoBehaviour
{

    public AudioClip[] hologramSounds;
    public Transform gunPos;


    public Material hologramMat;

    public Material[] materials;

 

    public GameObject gun;

    int totalMeshes = 0;
    public int completeMeshes = 0;

    bool isReady;

    bool gunPickedUp;

    void Start()
    {
        GetComponent<AudioSource>().Play();
        foreach (MeshRenderer mesh in GetComponentsInChildren<MeshRenderer>())
        {
            mesh.material = new Material(hologramMat);
            mesh.material.SetFloat("_FaderInOut", 0f);

            totalMeshes++;

        }
    }

   
    void Update()
    {
        if (totalMeshes > completeMeshes && !isReady)
        {
            FadeIN();
            

        }
        else if (!isReady)
        {
            isReady = true;
        }

      
        else if (gunPickedUp)
        {
          
            if (totalMeshes > completeMeshes)
            {
                FadeOUT();
            }
        }
    }

    void FadeOUT()
    {
        foreach (MeshRenderer mesh in GetComponentsInChildren<MeshRenderer>())
        {
            if (mesh.material.GetFloat("_FaderInOut") > 0)
            {
                mesh.material.SetFloat("_FaderInOut", mesh.material.GetFloat("_FaderInOut") - Time.deltaTime);

            }
            else
            {
                completeMeshes++;
            }
        }
    }

     void FadeIN()
    {
        foreach (MeshRenderer mesh in GetComponentsInChildren<MeshRenderer>())
        {
            if (mesh.material.GetFloat("_FaderInOut") < 1)
            {
                mesh.material.SetFloat("_FaderInOut", mesh.material.GetFloat("_FaderInOut") + Time.deltaTime);

            }
            else
            {
                completeMeshes++;
            }
        }
    }

  

    public void ResetTable()
    {
        completeMeshes = 0;
        isReady = false;
        gunPickedUp = false;

        GameObject newGun = Instantiate(gun, gunPos.position, Quaternion.identity);
        GetComponent<MeshCollider>().enabled = true;
       
    }
}
