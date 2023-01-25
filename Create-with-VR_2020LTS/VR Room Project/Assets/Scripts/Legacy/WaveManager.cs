using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class WaveManager : MonoBehaviour
{
    public GameObject[] maps;
    public GameObject AI;
    public GameObject box;
    public List<GameObject> currentEnemies = new List<GameObject>();
    
    public Transform[] SpawnPoints;
    public List<Transform> TempSpawnPoints = new List<Transform>();
    public int spawnCount;
    public int WaveNumber = 1;
    [SerializeField]
    bool playerHasGun;
    [SerializeField]
    bool waveActive = false;

    int[] numberAI = {1,2,3,4,6,8,10,14,16,20};

    public void PlayerReady()
    {
        StartCoroutine(StartWave());
        box.GetComponent<ObjectFade>().StartCoroutine(box.GetComponent<ObjectFade>().FadeOut());
        foreach (ObjectFade fadeScript in maps[0].GetComponentsInChildren<ObjectFade>())
        {
            fadeScript.StartFade();
            fadeScript.gameObject.GetComponent<MeshCollider>().enabled = true;
        }
    }

    IEnumerator StartWave()
    {
        yield return new WaitForSeconds(3);
        SpawnWave();
    }

    public void Update()
    {
      
    }

    void SpawnWave()
    {
        foreach(Transform t in SpawnPoints)
        {
            TempSpawnPoints.Add(t);
        }
       
        waveActive = true;

        SpawnUnit();
    }

    void SpawnUnit()
    {
        if(spawnCount >= numberAI[WaveNumber])
        {
            //Finish Round
            return;
        }
        
        int random = Random.Range(0, TempSpawnPoints.Count - 1);
        GameObject enemy = Instantiate(AI, TempSpawnPoints[random].position, Quaternion.identity);
        currentEnemies.Add(enemy);
        TempSpawnPoints.RemoveAt(random);
        spawnCount++;
    }
   
    public void RemoveEnemy(GameObject enemy)
    {
        currentEnemies.Remove(enemy);
        SpawnUnit();
    }
    public void Reset()
    {
        waveActive = false;

        TempSpawnPoints.Clear();

        foreach (GameObject ai in GameObject.FindGameObjectsWithTag("AI"))
        {
            if(ai.GetComponent<AI>()!=null)
            ai.GetComponent<AI>().Shot();
        }
        foreach (GameObject bullet in GameObject.FindGameObjectsWithTag("Bullet"))
        {
            Destroy(bullet);
        }
        foreach (GameObject Gun in GameObject.FindGameObjectsWithTag("Gun"))
        {
            Destroy(Gun);
        }
       
       
        
    }
}
