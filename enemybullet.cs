using UnityEngine;

public class enemybullet : MonoBehaviour
{
    public int damage = 15;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<playerhealthsystem>().TakeDamage(damage);
            Debug.Log("player hit!");

            Destroy(gameObject);
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject);
        }
    }
}











