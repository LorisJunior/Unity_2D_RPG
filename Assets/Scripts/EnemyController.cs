using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public UIHealthBar UIHealth;
    public int maxHealth = 3;
    public GameObject coin;
    public GameObject heart;
    public GameObject gem;
    public int damage = -1;
    public float speed = 3f;
    public bool vertical;
    public float changeTime = 2f;

    int currentHealth;
    Animator animator;
    Rigidbody2D rigidbody2d;
    int direction = -1;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        timer = changeTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }

    void FixedUpdate() 
    {
        Vector2 position = rigidbody2d.position;

        if (vertical)
        {
            animator.SetFloat("Move Y", direction);
            animator.SetFloat("Move X", 0);
            position.y += speed * Time.deltaTime * direction;
        }
        else
        {
            animator.SetFloat("Move Y", 0);
            animator.SetFloat("Move X", direction);
            position.x += speed * Time.deltaTime * direction;
        }

        rigidbody2d.MovePosition(position);
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        PlayerController controller = other.gameObject.GetComponent<PlayerController>();

        if (controller != null)
        {
            controller.ChangeHealth(damage);
        }
    }

    void DropLoot()
    {
        int lootChance = Random.Range(1,101);

        if (lootChance <= 50)
            Instantiate(coin, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
        if (lootChance > 50 && lootChance <= 75)
            Instantiate(heart,rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
        if (lootChance > 75)
            Instantiate(gem,rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
    }

    void Die()
    {
        DropLoot();
        Destroy(gameObject);
    }

    public void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

        UIHealth.SetValue(currentHealth/(float)maxHealth);

        if (currentHealth == 0)
        {
            Die();
        }
    }
    
}
