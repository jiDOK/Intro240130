using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");
        Vector3 dir = (new Vector3(xInput, 0f, zInput)).normalized;
        //transform.position += dir * speed * Time.deltaTime;
        controller.SimpleMove(dir * speed);
    }
}
