using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShoot : MonoBehaviour
{
    public Transform bulletSpawn;

    public GameObject Bullet;

    public float fireRate = 1f;

    public AudioClip shootClip;

    public float bulletSpeed = 10f;

    private float currentTime;

    bool TimerStart = false;

    public List<GameObject> bulletsShot = new List<GameObject>();

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<AI>().isReady || GetComponent<AI>().isShot)
        {
            return;
        }


        if (TimerStart)
        {
            if(Time.time >= currentTime + fireRate)
            {
                Shoot();
                currentTime = Time.time+ fireRate;
            }
        }
    }

    void Shoot()
    {

        GetComponent<AudioSource>().Play();
        GameObject newBullet = Instantiate(Bullet, bulletSpawn.transform.position, Quaternion.identity);
        newBullet.GetComponentInChildren<Bullet>().speed = bulletSpeed;
        newBullet.GetComponentInChildren<Bullet>().bulletSpawn = bulletSpawn;
        bulletsShot.Add(newBullet);

    }

    public void StartTimer()
    {
        GetComponent<AudioSource>().clip = shootClip;
        currentTime = Time.time;  
        TimerStart = true;
        
    }
}
