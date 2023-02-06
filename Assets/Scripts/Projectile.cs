using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject particleVFX;
    float lifeTime = 1f;

    Rigidbody2D rigidbody2d;

    // Start is called before the first frame update
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    private void Update() 
    {
        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0f)
        {
            Destroy(gameObject);
        }
    }

    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        EnemyController controller = other.gameObject.GetComponent<EnemyController>();

        if (controller != null)
        {
            controller.ChangeHealth(-1);
        }
        
        Instantiate(particleVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
