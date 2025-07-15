using UnityEngine;

public class MeleeAttack : StateMachineBehaviour
{
    Transform player;
    float attackRange = 5f;  // Set your desired melee attack range
    float attackCooldown = 1f; // Cooldown time between attacks (1 second)
    float timer = 0f; // Timer to track cooldown

    // Event to trigger damage in your separate damage handling script
    public delegate void OnMeleeAttack();
    public static event OnMeleeAttack MeleeAttackEvent;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timer = 0f;  // Reset the cooldown timer
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player == null)
            return;

        float distance = Vector3.Distance(animator.transform.position, player.position);

        if (distance > attackRange)
        {
            // Player moved out of melee range, stop attacking and start chasing
            animator.SetBool("IsMeleeAttacking", false);
            animator.SetBool("IsChasing", true);
        }
        else
        {
            // Player is within melee range, continue looping the attack animation
            timer += Time.deltaTime;  // Increment the cooldown timer

            // Only trigger damage once every cooldown period (1 second)
            if (timer >= attackCooldown)
            {
                // Trigger the event to deal damage via the external damage script
                MeleeAttackEvent?.Invoke();  // Calling the event to notify the damage handler

                // Reset the cooldown timer
                timer = 0f;
            }

            animator.SetBool("IsMeleeAttacking", true);  // Keep the attack animation looping
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Reset the melee attack status when exiting
        animator.SetBool("IsMeleeAttacking", false);
    }
}
