using UnityEngine;
using UnityEngine.AI;

public class ChasePlayer : StateMachineBehaviour
{
    NavMeshAgent agent;
    Transform player;

    public float chaseSpeed = 3.5f;
    public float meleeRange = 2f;
    public float rangedRange = 6f;


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent.speed = chaseSpeed;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(player.position);

        float distance = Vector3.Distance(animator.transform.position, player.position);

        if (distance < 3.5f)
        {
           
            animator.SetBool("IsMeleeAttacking", true);
            animator.SetBool("IsChasing", false);
        }
       else if (distance > 10f)
        {
            animator.SetBool("IsRangedAttacking", true);
            animator.SetBool("IsChasing", false);
        }
            



    }
}
