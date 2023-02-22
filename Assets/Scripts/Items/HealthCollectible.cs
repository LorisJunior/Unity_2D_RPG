using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    [SerializeField] private SO_AudioManager audioManager;
    
    [SerializeField] private int recoveryAmount = 1;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        PlayerController controller = other.GetComponent<PlayerController>();

        if (controller != null)
        {
            //Verify if player health is full
            if (controller.Health < Settings.maxHealth)
            {
                EventHandler.CallChangeHealthEvent(recoveryAmount);
                EventHandler.CallPlaySoundEvent(audioManager.GetAudioClip(Settings.heartSound));
                Destroy(gameObject);
            }
        }
    }
}
