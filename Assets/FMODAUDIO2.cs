using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODAUDIO2 : StateMachineBehaviour
{

    FMOD.Studio.EventInstance dragonGlide;
    FMOD.Studio.ParameterInstance flying;
    FMOD.Studio.EventDescription description;



    // Use this for initialization
    void Awake() {
        dragonGlide = RuntimeManager.CreateInstance("event:/Dragon_Glide");
        dragonGlide.getParameter("Flying", out flying);
    }



    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        flying.setValue(0);
        dragonGlide.start();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        flying.setValue(1);
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
