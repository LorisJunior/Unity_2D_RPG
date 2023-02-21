using UnityEngine;

public class CoinCollectible : MonoBehaviour
{
    public AudioClip audioClip;
    public int amount = 1;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        PlayerController controller = other.GetComponent<PlayerController>();

        if (controller != null)    
        {
            controller.IncreaseCoin(amount);
            controller.PlaySound(audioClip);
            Destroy(gameObject);
        }
    }
}
