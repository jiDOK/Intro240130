using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public Bubble[] bubblePrefabs;
    public float xOffset;
    public float yOffset;
    Bubble[,] bubbles = new Bubble[8, 8];
    string info = "";


    void Start()
    {
        Bubble.Collided += OnCollided;
        for (int y = 4; y < 8; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                //float stagger = y % 2 == 0 ? 0.5f : 0f;
                int rndIdx = Random.Range(0, bubblePrefabs.Length);
                Bubble bubble = Instantiate(bubblePrefabs[rndIdx]);
                bubble.transform.position = new Vector3(x + xOffset, y + yOffset, 0);
                bubbles[x, y] = bubble;
                //bubble.GetComponent<Bubble>().Collided += OnCollided;
            }
        }
        //GameObject b = bubbles[0, 0];
        //bubbles[0, 0] = null;
        //Destroy(b);
    }

    public void OnCollided(Bubble b, Bubble o)
    {
        if (b.GetDir() == Vector3.zero) return;
        // stop movement
        b.SetDir(Vector3.zero);
        // place in position
        Vector3 pos = o.transform.position;
        pos = new Vector3(pos.x, pos.y - 1f, 0f);
        Vector2Int posInt = new Vector2Int((int)pos.x, (int)pos.y);
        b.transform.position = pos;
        // put in array
        info = posInt.ToString();
        if (posInt.x >= bubbles.GetLength(0) || posInt.y >= bubbles.GetLength(1) || posInt.x < 0 || posInt.y < 0)
        {
            info = "Game Over!";
            return;
        }
        bubbles[posInt.x, posInt.y] = b;
        //bubbles[posInt.x, posInt.y].transform.localScale = Vector3.one * 0.5f;
        // check neighbors recursively(?)
        b.CheckNeighbors(posInt, bubbles);

        for (int y = 0; y < bubbles.GetLength(1); y++)
        {
            for (int x = 0; x < bubbles.GetLength(0); x++)
            {
                if (bubbles[x, y] != null && bubbles[x, y].deleteMe)
                {
                    Destroy(bubbles[x, y].gameObject);
                    bubbles[x, y] = null;
                }
            }
        }




        //b.transform.position = new Vector3(Mathf.Ceil(pos.x), Mathf.Floor(pos.y), 0f);

    }

    private void OnGUI()
    {
        GUILayout.Label(info);
    }

    private void OnDrawGizmos()
    {
        for (int x = 0; x < bubbles.GetLength(0); x++)
        {
            for (int y = 0; y < bubbles.GetLength(1); y++)
            {
                Gizmos.DrawWireCube(new Vector3(x, y, 0f), Vector3.one);
            }
        }
    }
}
