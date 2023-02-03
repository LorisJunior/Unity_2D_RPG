using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public int amount = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        PlayerController player = other.GetComponent<PlayerController>();

        if (player != null)
        {
            if (player.Health < player.maxHealth)
            {
                player.ChangeHealth(amount);
                Destroy(gameObject);
            }
        }
    }
}
