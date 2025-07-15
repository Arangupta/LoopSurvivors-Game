using UnityEngine;

public class healthboost : MonoBehaviour
{
    public int healAmount = 20; // Amount of health restored
    public AudioClip healSound; // Healing sound effect

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if Player collides
        {
            playerhealthsystem playerHealth = other.GetComponent<playerhealthsystem>();

            if (playerHealth != null && playerHealth.currentHealth < playerHealth.maxHealth)
            {
                playerHealth.Heal(healAmount); // Increase health

                // Play healing sound
                if (healSound != null)
                    AudioSource.PlayClipAtPoint(healSound, transform.position);


            }
        }
        // Remove power-up after use
        Destroy(gameObject);



    }
}
