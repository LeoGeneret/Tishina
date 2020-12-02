using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IdleBehaviour : StateMachineBehaviour
{

    readonly float minTime = 0.3f;
    readonly float maxTime = 1;

    float timer = 0;

    string[] triggers = { "Idle_1", "Idle_2", "Idle_3" };
    string lastTrigger;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer <= 0)
        {
            RandomAnim(animator);
            timer = Random.Range(minTime, maxTime);
        }
        else
        {
            timer -= Time.deltaTime;
        }
    }

    void RandomAnim(Animator animator)
    {
        System.Random rnd = new System.Random();

        string[] checkTriggers;

        if (lastTrigger != "")
        {
            checkTriggers = triggers.Where(v => v != lastTrigger).ToArray();
        }
        else
        {
            checkTriggers = triggers;
        }

        int idle = rnd.Next(checkTriggers.Length);
        string trigger = checkTriggers[idle];
        animator.SetTrigger(trigger);

        lastTrigger = trigger;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
