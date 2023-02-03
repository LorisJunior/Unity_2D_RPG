using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int maxHealth = 5;
    public float speed = 3f;
    public int Health {get => currentHealth;}
    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;
    int currentHealth;
    int coinAmount = 0;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        // currentHealth = maxHealth;
        currentHealth = 3;
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

    public void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }

    public void IncreaseCoin(int amount)
    {
        coinAmount += amount;
        Debug.Log(coinAmount);
    }
}
