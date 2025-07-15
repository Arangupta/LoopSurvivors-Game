using UnityEngine;

public class RangeAttack : StateMachineBehaviour
{
    Transform player;
    public GameObject projectilePrefab;
    public Transform firePoint;
    AudioSource audioSource;
    public AudioClip attackSound;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        audioSource = animator.GetComponent<AudioSource>();

        if (audioSource && attackSound)
            audioSource.PlayOneShot(attackSound);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player == null)
            return;

        animator.transform.LookAt(player);

        float distance = Vector3.Distance(animator.transform.position, player.position);

        if (distance > 50f)   // Example attackRange
        {
            animator.SetBool("IsRangedAttacking", false);
        }
        else if (distance < 3.5)
        {
            animator.SetBool("IsRangedAttacking", false);
            animator.SetBool("IsMeleeAttacking", true);
        }
    }

    // THIS FUNCTION will be called by Animation Event
    public void FireProjectile()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        }
    }
}
