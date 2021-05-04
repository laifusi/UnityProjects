using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistractedState : StateMachineBehaviour
{
    private int id; //determina el tipo de distracción
    private Scrollbar bar;
    public float timeToRewind; //tiempo que tarda en distraerse si no se actúa
    private float timePassed; //tiempo que ha pasado desde el inicio de la distracción
    private float undistractValue = 0.2f; //valor de ataque a la distracción
    private LevelManager levelManager;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        id = animator.GetInteger("nextState");
        bar = animator.GetComponent<Transform>().GetComponentInChildren<Scrollbar>(true);
        bar.gameObject.SetActive(true);
        bar.size = 0f;
        timePassed = 0f;
        levelManager = animator.GetComponentInParent<LevelManager>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timePassed += Time.deltaTime;
        bar.size = timePassed / timeToRewind;

        if (Input.GetButtonDown("Fly") && id == 1 ||
            Input.GetButtonDown("Phone") && id == 2 ||
            Input.GetButtonDown("Sleep") && id == 3 ||
            Input.GetButtonDown("Talk") && id == 4)
        {
            timePassed -= undistractValue;
        }

        if (timePassed >= timeToRewind)
        {
            levelManager.Rewind();
            animator.SetBool("rewind", true);
        }

        if (timePassed < 0)
        {
            animator.SetBool("notDistracted", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bar.gameObject.SetActive(false);
        levelManager.SetDistractionActive(-1);
    }

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
