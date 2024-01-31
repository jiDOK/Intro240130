using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject[] bubblePrefabs;
    public float xOffset;
    GameObject[,] bubbles = new GameObject[8, 8];
    

    void Start()
    {
        Bubble.Collided += OnCollided;
        for (int y = 0; y < 4; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                float stagger = y % 2 == 0 ? 0.5f : 0f;
                int rndIdx = Random.Range(0, bubblePrefabs.Length);
                GameObject bubble = Instantiate(bubblePrefabs[rndIdx]);
                bubble.transform.position = new Vector3(x + xOffset + stagger, y - y * 0.145f, 0);
                bubbles[x,y] = bubble;
                //bubble.GetComponent<Bubble>().Collided += OnCollided;
            } 
        }
        //GameObject b = bubbles[0, 0];
        //bubbles[0, 0] = null;
        //Destroy(b);
    }

    public void OnCollided(Bubble b)
    {

    }

    void Update()
    {
    }
}
