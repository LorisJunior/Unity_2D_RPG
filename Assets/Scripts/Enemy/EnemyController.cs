using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private SO_AudioManager audioManager;
    [SerializeField] private SO_DropItems dropList;

    public GameObject deathVFX;
    public UIHealthBar UIHealth;
    public bool vertical;
    public float changeTime = 2f;

    private int currentHealth;
    private Animator animator;
    private Rigidbody2D rigidbody2d;
    private int direction = -1;
    private float changeDirectionTime = 0;

    private void Start()
    {
        currentHealth = Settings.enemyMaxHealth;
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        ChangeDirection();
        EnemyMovement();
    }

    // Change Direction of the enemy after some time
    private void ChangeDirection()
    {
        if (Time.time >= changeDirectionTime)
        {
            direction = -direction;
            changeDirectionTime = Time.time + changeTime;
        }
    }

    private void EnemyMovement()
    {
        Vector2 position = rigidbody2d.position;

        if (vertical)
        {
            animator.SetFloat(Settings.moveY, direction);
            animator.SetFloat(Settings.moveX, 0);
            position.y += Settings.enemySpeed * Time.deltaTime * direction;
        }
        else
        {
            animator.SetFloat(Settings.moveY, 0);
            animator.SetFloat(Settings.moveX, direction);
            position.x += Settings.enemySpeed * Time.deltaTime * direction;
        }

        rigidbody2d.MovePosition(position);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        bool isPlayer = other.gameObject.CompareTag(Tags.player);

        if (isPlayer)
        {
            Attack();
        }
    }

    private void Attack()
    {
        EventHandler.CallChangeHealthEvent(Settings.enemyDamage);
    }

    private void Die()
    {
        Instantiate(deathVFX, rigidbody2d.position + Vector2.up * 0.7f, Quaternion.identity);
        EventHandler.CallPlaySoundEvent(audioManager.GetAudioClip(Settings.mobDeathSound));
        DropLoot();
        Destroy(gameObject);
    }

    private void DropLoot()
    {
        int lootChance = Random.Range(1, 101);

        if (lootChance <= 50)
            Instantiate(dropList.GetDropItem(Settings.coin), rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
        if (lootChance > 50 && lootChance <= 75)
            Instantiate(dropList.GetDropItem(Settings.heart), rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
        if (lootChance > 75)
            Instantiate(dropList.GetDropItem(Settings.gem), rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
    }

    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Clamp(currentHealth + damage, 0, Settings.enemyMaxHealth);

        UIHealth.SetValue(currentHealth / (float)Settings.enemyMaxHealth);

        if (currentHealth == 0)
        {
            Die();
        }
    }
}
