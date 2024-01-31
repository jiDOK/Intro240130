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

    public void CheckNeighbors(Vector2Int pos, Bubble[,] bubbles)
    {
        for (int y = pos.y - 1; y <= pos.y + 1; y++)
        {
            for (int x = pos.x - 1; x <= pos.x + 1; x++)
            {
                if (y < 0 || x < 0 || y >= bubbles.GetLength(1) || x >= bubbles.GetLength(0)|| bubbles[x,y] == null) continue;
                if(bColor == bubbles[x, y].bColor)
                {
                    deleteMe = true;
                    bubbles[x, y].deleteMe = true;
                    //bubbles[x,y].CheckNeighbors(new Vector2Int(x,y), bubbles);
                }
            }

        }
    }
}

public enum BColor
{
    Blue, Green, Red, Yellow
}
