using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 3f;

    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    void FixedUpdate() 
    {
        Vector2 position = rigidbody2d.position;

        position.x += speed * Time.deltaTime * horizontal;
        position.y += speed * Time.deltaTime * vertical;

        rigidbody2d.MovePosition(position);
    }
}
