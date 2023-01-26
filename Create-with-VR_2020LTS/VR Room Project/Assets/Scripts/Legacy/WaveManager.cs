using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class WaveManager : MonoBehaviour
{
    public GameObject[] maps;
    public GameObject[] AI;
    public GameObject box;
    public GameObject gun;

    public Transform gunPos;
    public List<GameObject> currentEnemies = new List<GameObject>();

    public Transform[] SpawnPoints;
    public List<Transform> TempSpawnPoints = new List<Transform>();
    public int spawnCount;
    public int WaveNumber = 1;
    [SerializeField]
    bool playerHasGun;
    [SerializeField]
    bool waveActive = false;

    int[] numberAI = { 0, 2, 3, 4, 6, 8, 10, 14, 16, 20 };

    public void PlayerReady()
    {
        //if wave currently running return
        if (waveActive)
        {
            return;
        }

        //Start Wave 
        StartCoroutine(StartWave());
        //Hide the box 
        box.GetComponent<ObjectFade>().StartFadeOut();

        //Map fades in
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

    void SpawnWave()
    {
        foreach (Transform t in SpawnPoints)
        {
            TempSpawnPoints.Add(t);
        }

        waveActive = true;

        SpawnUnit();
    }

    public void SpawnUnit()
    {
        if (spawnCount >= numberAI[WaveNumber] && waveActive)
        {
            ResetWave();
            return;
        }

        int random = Random.Range(0, TempSpawnPoints.Count);
        int random2 = Random.Range(0, AI.Length);
        GameObject enemy = Instantiate(AI[random2], TempSpawnPoints[random].position, Quaternion.identity);

        currentEnemies.Add(enemy);
        TempSpawnPoints.RemoveAt(random);

        spawnCount++;
    }

    public void RemoveEnemy(GameObject enemy)
    {
        if (currentEnemies.Contains(enemy))
            currentEnemies.Remove(enemy);

    }

    public void ResetWave()
    {
        TempSpawnPoints.Clear();
        currentEnemies.Clear();

        foreach (ObjectFade fadeScript in maps[0].GetComponentsInChildren<ObjectFade>())
        {
            fadeScript.StartFadeOut();
            fadeScript.gameObject.GetComponent<MeshCollider>().enabled = false;
        }

        foreach (GameObject ai in GameObject.FindGameObjectsWithTag("AI"))
        {
            if (ai.GetComponent<AI>() != null)
                ai.GetComponent<AI>().FadeOut();
        }
        foreach (GameObject bullet in GameObject.FindGameObjectsWithTag("Bullet"))
        {
            Destroy(bullet);
        }
        foreach (GameObject gun in GameObject.FindGameObjectsWithTag("Object"))
        {
            Destroy(gun);
        }

        spawnCount = 0;

        GameObject newGun = Instantiate(gun, gunPos.position, gunPos.rotation);
        newGun.GetComponent<Gun>().isSimulatorGun = true;
      
        waveActive = false;
        box.GetComponent<ObjectFade>().StartFade();
        box.GetComponent<MeshCollider>().enabled = true;
    }
}
