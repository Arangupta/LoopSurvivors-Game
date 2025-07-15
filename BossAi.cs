using UnityEngine;
using UnityEngine.AI;
using static Unity.VisualScripting.Member;

public class BossAI : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;
    private Transform player;

    public float meleeRange = 2.5f;
    public float rangeAttackMinRange = 2.5f;
    public float rangeAttackMaxRange = 10f;
    public float rotationSpeed = 5f;  // Speed at which the enemy turns to face the player
   

    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);
        Vector3 directionToPlayer = player.position - transform.position;
        directionToPlayer.y = 0;  // Make sure we're only considering X and Z (not height difference)

        // Rotate the enemy towards the player
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        // Check if enemy is facing the player
        float angle = Vector3.Angle(transform.forward, directionToPlayer);

        if (distance > rangeAttackMaxRange)
        {
            // Player far: Chase
            SetOnlyThisBool("IsChasing");
        

            agent.isStopped = false;
            // Allow movement
        }
        else if (distance > meleeRange && distance <= rangeAttackMaxRange && angle < 45f)
        {
            // Player in medium range AND facing the player: Range Attack
            SetOnlyThisBool("IsRangedAttacking");
            agent.isStopped = true;  // Stop movement during range attack
        }
        else if (distance <= meleeRange)
        {
            // Player close: Melee Attack
            SetOnlyThisBool("IsMeleeAttacking");
            agent.isStopped = true;  // Stop movement during melee attack
        }
        else
        {
            // Player not in correct range or facing direction: Do nothing or go back to idle
            SetOnlyThisBool("IsChasing");
        }
    }

    private void SetOnlyThisBool(string boolName)
    {
        animator.SetBool("IsChasing", false);
        animator.SetBool("IsRangedAttacking", false);
        animator.SetBool("IsMeleeAttacking", false);

        animator.SetBool(boolName, true); // Enable only the correct one
    }
}
