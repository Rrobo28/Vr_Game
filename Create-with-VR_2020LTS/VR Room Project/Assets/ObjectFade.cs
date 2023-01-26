using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFade : MonoBehaviour
{
    public Material holoMaterial;

    private Material[] prevMaterials;

    public bool StartShown = false;

    private void Start()
    {
        prevMaterials = GetComponent<MeshRenderer>().materials;

        StartCoroutine(SetSimMaterial(StartShown));
    }

   public void StartFade()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        foreach (MeshRenderer mesh in GetComponentsInChildren<MeshRenderer>())
        {
            Material[] materials = mesh.materials;

            for (int i = 0; i < materials.Length; i++)
            {
                float num = 0;
              
                while(materials[i].GetFloat("_FaderInOut") < 1)
                {
                    num += Time.time / 100;
                    materials[i].SetFloat("_FaderInOut", num);
                    yield return null;
                }
            }
            mesh.materials = materials;
        }
        StartCoroutine(SetMaterials());

    }

    public void StartFadeOut()
    {
        StartCoroutine(SetSimMaterial(true));
        StartCoroutine(FadeOut());
    }

    public IEnumerator FadeOut()
    {
        foreach (MeshRenderer mesh in GetComponentsInChildren<MeshRenderer>())
        {
            Material[] materials = mesh.materials;

            for (int i = 0; i < materials.Length; i++)
            {
                float num = 0;

                while (materials[i].GetFloat("_FaderInOut") > 0)
                {
                    num -= Time.time / 100;
                    materials[i].SetFloat("_FaderInOut", num);
                    yield return null;
                }
            }
            mesh.materials = materials;
            GetComponent<MeshCollider>().enabled = false;
        }
    }

    IEnumerator SetMaterials()
    {
        foreach (MeshRenderer mesh in GetComponentsInChildren<MeshRenderer>())
        {
            Material[] materials = mesh.materials;

            for (int i = 0; i < materials.Length; i++)
            {
                materials[i] = prevMaterials[i];
            }
            mesh.materials = materials;
        }
        yield return null;
    }
    IEnumerator SetSimMaterial(bool shown)
    {
        foreach (MeshRenderer mesh in GetComponentsInChildren<MeshRenderer>())
        {
            Material[] materials = mesh.materials;

            for (int i = 0; i < materials.Length; i++)
            {
                materials[i] = new Material(holoMaterial);
                if (shown)
                {
                    materials[i].SetFloat("_FaderInOut", 1);
                }
                else
                {
                    materials[i].SetFloat("_FaderInOut", 0);
                }
            }
            mesh.materials = materials;
        }
        yield return null;
       
    }
   

}
