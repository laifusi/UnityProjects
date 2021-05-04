using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WatchTVState : StateMachineBehaviour
{
    private float timeAtStart;
    private float randomWaitTime;
    private LevelManager levelData;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetInteger("nextState", 0);
        animator.SetBool("notDistracted", false);
        animator.SetBool("rewind", false);
        levelData = animator.GetComponentInParent<LevelManager>();
        timeAtStart = Time.time;
        randomWaitTime = Random.Range(levelData.minTimeRange,levelData.maxTimeRange);
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float timeSinceStart = Time.time - timeAtStart;
        if(levelData.GetDistractionActive() > 0)
        {
            randomWaitTime = Random.Range(levelData.maxTimeRange, levelData.maxTimeRange * 2); //Si hay otra distracción, incrementamos el tiempo para la distracción del siguiente personaje
        }
        if(timeSinceStart >= randomWaitTime && !levelData.levelended)
        {
            if(levelData.AddDistraction())
            {
                int next = Random.Range(1, levelData.possibleDistractions + 1);
                animator.SetInteger("nextState", next);
                levelData.SetDistractionActive(1);
            }
        }
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
