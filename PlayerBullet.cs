using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float damage = 10f;
    public float lifetime = 5f; 

    private void Start()
    {
        Destroy(gameObject, lifetime); // Auto destroy bullet after some time
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("bullet hit!:"+other.name);
        if (other.CompareTag("Enemy")) // Check if hit enemy
        {
            Enemyhelth enemy = other.GetComponent<Enemyhelth>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }

        Destroy(gameObject); // Destroy bullet on impact
    }
}