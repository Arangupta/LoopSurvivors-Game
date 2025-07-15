using UnityEngine;

public class BossShotReference : MonoBehaviour
{
    public Transform firePoint;
    public GameObject projectilePrefab;
    public float projectileForce = 700f;
    public AudioSource source;
    public AudioClip clip;

    public void Shoot()
    {

        if (firePoint == null || projectilePrefab == null)
        {
            Debug.LogWarning("Missing firePoint or projectilePrefab!");
            return;
        }

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (source && clip)
        {

            source.PlayOneShot(clip);
        }


        if (rb != null)
        {
            Vector3 direction = (GetPlayerPosition() - firePoint.position).normalized;
            rb.AddForce(direction * projectileForce);
        }
    }

    Vector3 GetPlayerPosition()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            return player.transform.position;
        return firePoint.position + firePoint.forward; // fallback
    }
}
