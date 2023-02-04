using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float timeInvincible = 2f;
    public int maxHealth = 5;
    public float speed = 3f;
    public int Health {get => currentHealth;}
    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;
    int currentHealth;
    int coinAmount = 0;
    bool isInvincible;
    float invincibleTimer;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        //Deactivate invincible mode after some seconds
        InvincibleTime();
    }

    void FixedUpdate() 
    {
        Vector2 position = rigidbody2d.position;

        position.x += speed * Time.deltaTime * horizontal;
        position.y += speed * Time.deltaTime * vertical;

        rigidbody2d.MovePosition(position);
    }

    void InvincibleTime()
    {
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;

            if (invincibleTimer < 0)
            {
                isInvincible = false;
            }
        }
    }
    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if(isInvincible)
                return;

            //Activate player invincible mode

            isInvincible = true;
            invincibleTimer = timeInvincible;
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }

    public void IncreaseCoin(int amount)
    {
        coinAmount += amount;
        Debug.Log(coinAmount);
    }
}
