using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public float rotSpeed = 30f;
    public Bubble[] bubblePrefabs;
    Transform spawnPoint;
    float curAngle;
    Bubble nextBubble;

    void Start()
    {
        spawnPoint = transform.Find("SpawnPoint");
        int rndIdx = Random.Range(0, bubblePrefabs.Length);
        nextBubble = Instantiate(bubblePrefabs[rndIdx], transform.position + Vector3.right * 4, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            nextBubble.transform.position = spawnPoint.position;
            nextBubble.SetDir(transform.up);
            int rndIdx = Random.Range(0, bubblePrefabs.Length);
            nextBubble = Instantiate(bubblePrefabs[rndIdx], transform.position + Vector3.right * 4, Quaternion.identity);
        }
        curAngle -= Input.GetAxis("Horizontal") * rotSpeed * Time.deltaTime;
        curAngle = Mathf.Clamp(curAngle, -85f, 85f);
        transform.eulerAngles = new Vector3(0f, 0f, curAngle);
    }
}
