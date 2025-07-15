using UnityEngine;

public class EnemyHealthsystem : MonoBehaviour
{
    public int health = 3; // Base health
    private static int healthMultiplier = 0; // Increases every loop

   // public GameObject deathEffect; // Assign explosion or blood effect prefab

    void Start()
    {
        health += healthMultiplier; // Increase health based on difficulty
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {

        Destroy(gameObject);
    }

    public static void IncreaseDifficulty()
    {
        healthMultiplier += 2; // Increase enemy health after each loop
    }
}
