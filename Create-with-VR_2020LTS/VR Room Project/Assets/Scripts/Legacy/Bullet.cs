using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Material bulletMat;

    public Transform bulletSpawn;

    public float speed;

    private void Start()
    {
        GetComponentInChildren<MeshRenderer>().material = new Material(bulletMat);
        Vector3 direction = Camera.main.transform.position-bulletSpawn.transform.position;
        transform.forward =direction.normalized;

        GetComponent<Rigidbody>().AddForce(direction.normalized * speed);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MainCamera"))
        {
            GameObject.Find("GameMode").GetComponent<WaveManager>().Reset();
            Destroy(this.gameObject);
        }
    }
}
