using System;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public static event Action<Bubble, Bubble> Collided;
    public float speed = 10f;
    public BColor bColor;
    public bool deleteMe;
    Vector3 dir;

    void OnTriggerEnter(Collider other)
    {
        Bubble b = other.gameObject.GetComponent<Bubble>();
        if (b != null)
        {
            Collided?.Invoke(this, b);
        }
    }

    void Update()
    {
        transform.position += dir * speed * Time.deltaTime;
    }

    public void SetDir(Vector3 dir)
    {
        this.dir = dir;
    }

    public Vector3 GetDir()
    {
        return dir;
    }
}

public enum BColor
{
    Blue, Green, Red, Yellow
}
