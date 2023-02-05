using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public int amount = 1;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        PlayerController controller = other.GetComponent<PlayerController>();

        if (controller != null)
        {
            if (controller.Health < controller.maxHealth)
            {
                controller.ChangeHealth(amount);
                Destroy(gameObject);
            }
        }
    }
}
