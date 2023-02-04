using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject arrowPrefab;
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
    Animator animator;
    Vector2 lookDirection = new Vector2(1,0);
    Vector3 zAxis = new Vector3(0, 0, 1);
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
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

        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0) || !Mathf.Approximately(move.y, 0))
        {
            lookDirection = move;
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
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

    void Attack()
    {
        GameObject arrowObject = Instantiate(arrowPrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        RotateArrow(ref arrowObject);

        Projectile arrow = arrowObject.GetComponent<Projectile>();
        arrow.Launch(lookDirection, 300f);

        animator.SetTrigger("Attack");
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

            animator.SetTrigger("Hit");
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }

    public void IncreaseCoin(int amount)
    {
        coinAmount += amount;
        Debug.Log(coinAmount);
    }
    
    void RotateArrow(ref GameObject arrowObject)
    {
        if (lookDirection.x == 1)
            arrowObject.transform.rotation = Quaternion.AngleAxis(-90, zAxis);
        if (lookDirection.x == -1)
            arrowObject.transform.rotation = Quaternion.AngleAxis(90, zAxis);
        if (lookDirection.y == 1)
            arrowObject.transform.rotation = Quaternion.identity;
        if (lookDirection.y == -1)
            arrowObject.transform.rotation = Quaternion.AngleAxis(180, zAxis);
    }
}
