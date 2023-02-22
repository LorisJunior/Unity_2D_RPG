using UnityEngine;

public class CoinCollectible : MonoBehaviour
{
    [SerializeField] private SO_AudioManager audioManager;
    [SerializeField] private int coinAmount = 1;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag(Tags.player))
        {
            EventHandler.CallIncreaseCoinEvent(coinAmount);
            EventHandler.CallPlaySoundEvent(audioManager.GetAudioClip(Settings.collectableSound));
            Destroy(gameObject);
        }
    }
}
