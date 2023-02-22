using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private SO_AudioManager audioManager;

    public UICoinText coinText;
    public UIHealthBar UIHealth;
    public GameObject arrowPrefab;
    public int Health => currentHealth;

    private AudioSource audioSource;
    private Rigidbody2D rigidbody2d;
    private Animator animator;
    private int currentHealth;
    private float horizontal;
    private float vertical;
    private int coinAmount = 0;
    private float nextAttackTime = 0f;
    private float nextDamageTime = 0f;
    private Vector2 lookDirection = new Vector2(1, 0);

    private void OnEnable()
    {
        EventHandler.PlaySoundEvent += PlaySound;
        EventHandler.IncreaseCoinEvent += IncreaseCoin;
        EventHandler.ChangeHealthEvent += ChangeHealth;
    }

    private void OnDisable()
    {
        EventHandler.PlaySoundEvent -= PlaySound;
        EventHandler.IncreaseCoinEvent -= IncreaseCoin;
        EventHandler.ChangeHealthEvent -= ChangeHealth;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        currentHealth = Settings.maxHealth;
    }

    private void Update()
    {
        #region Player Input

        PlayerMovementInput();

        LaunchArrow();

        Interact();

        #endregion
    }

    private void FixedUpdate()
    {
        PlayerMovement();
    }

    private void PlayerMovementInput()
    {
        // Horizontal and Vertical input

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        // If player is moving in diagonal adjust speed

        if (horizontal != 0 && vertical != 0)
        {
            horizontal *= 0.71f;
            vertical *= 0.71f;
        }

        // Get player look direction and when is moving

        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0) || !Mathf.Approximately(move.y, 0))
        {
            lookDirection = move;
            lookDirection.Normalize();
        }

        // Set animator parameters

        animator.SetFloat(Settings.lookX, lookDirection.x);
        animator.SetFloat(Settings.lookY, lookDirection.y);
        animator.SetFloat(Settings.animSpeed, move.magnitude);
    }

    private void PlayerMovement()
    {
        Vector2 position = rigidbody2d.position;

        position.x += Settings.playerSpeed * Time.deltaTime * horizontal;
        position.y += Settings.playerSpeed * Time.deltaTime * vertical;

        rigidbody2d.MovePosition(position);
    }

    private void LaunchArrow()
    {
        if (Time.time >= nextAttackTime && Input.GetKeyDown(KeyCode.Space))
        {
            //Rotate the arrow in the look direction
            float angle = Vector2.SignedAngle(Vector2.up, lookDirection);
            GameObject arrowObject = Instantiate(arrowPrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.AngleAxis(angle, Vector3.forward));

            Projectile arrow = arrowObject.GetComponent<Projectile>();
            arrow.Launch(lookDirection, Settings.projectileForce);

            //Trigger Launch animation and Arrow sound
            animator.SetTrigger(Settings.launch);
            PlaySound(audioManager.GetAudioClip(Settings.arrowSound));

            nextAttackTime = Time.time + Settings.attackRate;
        }
    }

    private void Interact()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));

            //If you are 1.5m of the NPC display a dialog quest
            if (hit.collider != null)
            {
                Quest quest = hit.collider.GetComponent<Quest>();

                if (quest != null)
                {
                    quest.DisplayQuest();
                    PlaySound(audioManager.GetAudioClip(Settings.questSound));
                }
            }
        }
    }

    private void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (Time.time < nextDamageTime)
                return;

            TakeDamage();

            // Next time player can take damage
            nextDamageTime = Time.time + Settings.timeInvincible;
        }

        UpdateHealth(amount);

        if (currentHealth == 0)
        {
            // If player die reload the level
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void TakeDamage()
    {
        animator.SetTrigger(Settings.hit);
        PlaySound(audioManager.GetAudioClip(Settings.hurtSound));
    }

    private void UpdateHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, Settings.maxHealth);
        UIHealth.SetValue(currentHealth / (float)Settings.maxHealth);
    }

    private void IncreaseCoin(int amount)
    {
        coinAmount += amount;
        coinText.IncreaseCoinUI(coinAmount);
    }

    private void PlaySound(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
}
