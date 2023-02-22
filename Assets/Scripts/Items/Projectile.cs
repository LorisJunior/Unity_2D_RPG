using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject particleVFX;
    public int damage = -1;

    private float lifeTime = 1f;
    private Rigidbody2D rigidbody2d;

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        StartCoroutine(DestroyAfterTime(lifeTime));
    }

    private IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        EnemyController controller = other.gameObject.GetComponent<EnemyController>();
        
        bool isEnemy = controller != null;

        if (isEnemy)
            CauseDamage(controller);
        
        Instantiate(particleVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void CauseDamage(EnemyController controller)
    {
        controller.TakeDamage(damage);
    }

    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
    }
}
