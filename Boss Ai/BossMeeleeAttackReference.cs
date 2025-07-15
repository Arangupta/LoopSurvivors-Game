using UnityEngine;
using static Unity.VisualScripting.Member;

public class BossMeeleeAttackReference : MonoBehaviour
{
    playerhealthsystem playerHealth;
    public int damage = 20;
    public float attackRange = 3.5f;
    public AudioSource source;
    public AudioClip clip;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerHealth = player.GetComponent<playerhealthsystem>();

        }
    }
    public void sound()
    {

        if (source && clip)
        {

            source.PlayOneShot(clip);
        }
    }

    public void dealdamage()
    {
        if (playerHealth == null) return;

        float distance = Vector3.Distance(transform.position, playerHealth.transform.position);

        if (distance <= attackRange)
        {
            playerHealth.TakeDamage(damage);
            Debug.Log("Enemy dealt damage with animation hit!");
        }
    }
}
