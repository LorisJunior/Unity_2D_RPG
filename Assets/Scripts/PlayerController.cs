using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float attackInterval = 0.5f;
    public UICoinText coinText;
    public UIHealthBar UIHealth;
    public GameObject arrowPrefab;
    public float timeInvincible = 2f;
    public int maxHealth = 5;
    public float speed = 3f;
    public int Health {get => currentHealth;}

    float attackTimer;
    bool canAttack = true;
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
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        

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

        if (!canAttack)
        {
            attackTimer -= Time.deltaTime;

            if (attackTimer < 0)
                canAttack = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && canAttack)
        {
            Attack();
            canAttack = false;
            attackTimer = attackInterval;
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

        //Rotate the arrow in the look direction
        float angle = Vector2.SignedAngle(Vector2.right, lookDirection) -90f;
        arrowObject.transform.rotation = Quaternion.AngleAxis(angle, zAxis);

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
        UIHealth.SetValue(currentHealth/(float)maxHealth);
    }

    public void IncreaseCoin(int amount)
    {
        coinAmount += amount;
        coinText.IncreaseCoinUI(coinAmount);
    }
}
