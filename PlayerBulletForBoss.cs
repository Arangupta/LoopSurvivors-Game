using UnityEngine;

public class PlayerBulletForBoss : MonoBehaviour
{
    public int damage = 10;
    public float lifetime = 5f;

    private void Start()
    {
        Destroy(gameObject, lifetime); // Auto destroy bullet after some time
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("bullet hit!:" + other.name);
        if (other.CompareTag("Enemy")) // Check if hit enemy
        {
            BossHealth enemy = other.GetComponent<BossHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }

        Destroy(gameObject); // Destroy bullet on impact
    }
}