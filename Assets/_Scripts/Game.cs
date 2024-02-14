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
        Vector3 bPos = b.transform.position;
        Vector3 oPos = o.transform.position;
        Vector3 newPos = Vector3.zero;
        if (Mathf.Abs(bPos.y - oPos.y) < Mathf.Abs(bPos.x - oPos.x))
        {
            float newX = oPos.x + Mathf.Sign(bPos.x - oPos.x);
            newPos = new Vector3(newX, oPos.y, 0f);
        }
        else
        {
            newPos = new Vector3(oPos.x, oPos.y - 1f, 0f);
        }
        Vector2Int posInt = new Vector2Int((int)newPos.x, (int)newPos.y);
        b.transform.position = newPos;
        // put in array
        info = posInt.ToString();
        if (posInt.x >= bubbles.GetLength(0) || posInt.y >= bubbles.GetLength(1) || posInt.x < 0 || posInt.y < 0)
        {
            info = "Game Over!";
            return;
        }
        bubbles[posInt.x, posInt.y] = b;
        // use breadth first search to find path
        HashSet<Vector2Int> reached = BFS(posInt);
        if (reached.Count < 3) return;
        foreach (Vector2Int coords in reached)
        {
            Destroy(bubbles[coords.x, coords.y].gameObject);
            bubbles[coords.x, coords.y] = null;
        }
    }

    HashSet<Vector2Int> BFS(Vector2Int startPos)
    {
        Queue<Vector2Int> frontier = new Queue<Vector2Int>();
        HashSet<Vector2Int> reached = new HashSet<Vector2Int>();
        frontier.Enqueue(startPos);
        reached.Add(startPos);

        while (frontier.Count > 0)
        {
            Vector2Int current = frontier.Dequeue();
            foreach (Vector2Int neighbor in GetNeighbors(current))
            {
                if (!reached.Contains(neighbor))
                {
                    frontier.Enqueue(neighbor);
                    reached.Add(neighbor);
                }
            }
        }
        return reached;
    }

    List<Vector2Int> GetNeighbors(Vector2Int start)
    {
        BColor bColor = bubbles[start.x, start.y].bColor;
        List<Vector2Int> neighbors = new List<Vector2Int>();
        for (int y = start.y - 1; y < start.y + 2; y++)
        {
            for (int x = start.x - 1; x < start.x + 2; x++)
            {
                if (x >= bubbles.GetLength(0) || y >= bubbles.GetLength(1) || x < 0 || y < 0)
                {
                    continue;
                }
                if (!(x == start.x && y == start.y) && bubbles[x, y] != null && bubbles[x, y].bColor == bColor)//bubble color auch noch!
                {
                    neighbors.Add(new Vector2Int(x, y));
                }
            }
        }
        return neighbors;
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
