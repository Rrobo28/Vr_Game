using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public Material hologramMat;

    public Material[] materials;

    public MeshRenderer[] gunMesh;

    private Animator thisAnim;

    int totalMeshes = 0;
    int completeMeshes = 0;

    public bool isReady;
    public bool isShot;
    private void Start()
    {
        thisAnim = GetComponent<Animator>();
        GetComponent<AudioSource>().Play();
        foreach (SkinnedMeshRenderer mesh in GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            mesh.material = new Material(hologramMat);
            mesh.material.SetFloat("_FaderInOut", 0f);

            totalMeshes++;

        }
        foreach (MeshRenderer mesh in gunMesh)
        {
            mesh.material = new Material(hologramMat);
            mesh.material.SetFloat("_FaderInOut", 0f);

            totalMeshes++;

        }
    }

    private void Update()
    {
        transform.LookAt(Camera.main.transform.position);
        if (totalMeshes > completeMeshes && !isReady)
        {
            FadeIn();
        }
        else if (!isReady)
        {
            isReady = true;
            GetComponent<AIShoot>().StartTimer();
        }
        if(isReady && isShot)
        {
            GameObject.Find("GameMode").GetComponent<WaveManager>().RemoveEnemy(this.gameObject);
            FadeOut();
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        thisAnim.SetLookAtWeight(1);
        thisAnim.SetLookAtPosition(Camera.main.transform.position);
    }

    public void Shot()
    {
        if (!isReady)
            return;

        foreach(GameObject bullet in GetComponent<AIShoot>().bulletsShot)
        {
            Destroy(bullet);
        }

        completeMeshes = 0;

        isShot = true;

       
       
    }

    void FadeIn()
    {
        foreach (SkinnedMeshRenderer mesh in GetComponentsInChildren<SkinnedMeshRenderer>())
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
        foreach (MeshRenderer mesh in gunMesh)
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


    public void FadeOut()
    {

        if (totalMeshes > completeMeshes)
        {
            foreach (SkinnedMeshRenderer mesh in GetComponentsInChildren<SkinnedMeshRenderer>())
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
            foreach (MeshRenderer mesh in gunMesh)
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
        else
        {
            Destroy(this.gameObject);
        }
       
    }

   

}
