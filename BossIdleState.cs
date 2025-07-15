using System.Collections.Generic;
using UnityEngine;

public class IdleState : StateMachineBehaviour
{
    float Timer;
    Transform player;
    float chase = 8;
   
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        Timer = 0;
        player = GameObject.FindGameObjectWithTag("Player").transform;


    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
          Timer += Time.deltaTime;
        if (Timer > 5)
            animator.SetBool("IsPatrolling", true);

      
        



    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

  
}
