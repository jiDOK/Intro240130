using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Bubble b = other.gameObject.GetComponent<Bubble>();
        if(b != null)
        {
            Destroy(b.gameObject);
            //Debug.Log("collision");
        }
    }
}
