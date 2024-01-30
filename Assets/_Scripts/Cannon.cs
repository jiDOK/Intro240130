using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public float rotSpeed = 30f;
    public Bubble[] bubblePrefabs;
    Transform spawnPoint;
    float curAngle;

    void Start()
    {
        spawnPoint = transform.Find("SpawnPoint");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int rndIdx = Random.Range(0, bubblePrefabs.Length);
            Bubble bubble = Instantiate(bubblePrefabs[rndIdx], spawnPoint.position, Quaternion.identity);
        }
        curAngle -= Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0f, 0f, curAngle);
    }
}
