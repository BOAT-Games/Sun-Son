using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamStateBehavior : StateMachineBehaviour
{
    public ParamStateData[] paramData;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach(ParamStateData p in paramData)
        {
            animator.SetBool(p.paramName, p.value);
        }
    }

    [Serializable]
    public struct ParamStateData
    {
        public string paramName;
        public bool value;
    }
}
