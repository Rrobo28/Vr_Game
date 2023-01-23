using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class WaveManager : MonoBehaviour
{

    public GameObject AI;

    public List<GameObject> currentEnemies = new List<GameObject>();
    public GameObject Table;

     public GameObject gun;

    public Transform[] SpawnPoints;
    public List<Transform> TempSpawnPoints = new List<Transform>();

    public int WaveNumber = 1;
    [SerializeField]
    bool playerHasGun;
    [SerializeField]
    bool waveActive = false;

    int[] numberAI = {1,2,3,4,6,8,10,14,16,20};


 


    public void Update()
    {
       
        if (waveActive && currentEnemies.Count == 0)
        {
            Reset();
        }
    }

    void SpawnWave()
    {
        foreach(Transform t in SpawnPoints)
        {
            TempSpawnPoints.Add(t);
        }
       
        waveActive = true;
        for (int i = 0; i < numberAI[WaveNumber]; i++) 
        {

            int random = Random.Range(0, TempSpawnPoints.Count - 1);
            GameObject enemy = Instantiate(AI, TempSpawnPoints[random].position, Quaternion.identity);
            currentEnemies.Add(enemy);
            TempSpawnPoints.RemoveAt(random);
        }

    }
   
    public void RemoveEnemy(GameObject enemy)
    {
        currentEnemies.Remove(enemy);
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
        Table.GetComponent<TableFade>().ResetTable();
        playerHasGun =false;
        waveActive = false;
    }
}
