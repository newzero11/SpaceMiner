using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendBehaviour : StateMachineBehaviour
{

    public string stateName;
    public bool resetBlendValueOnExit;
    [Range(0f, 1f)] public float blendValue;

    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("State: " + stateName);
    }

    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (resetBlendValueOnExit)
            Actions.setBlend?.Invoke(blendValue);
    }

}
