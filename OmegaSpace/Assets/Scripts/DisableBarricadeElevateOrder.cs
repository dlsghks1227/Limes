using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableBarricadeElevateOrder : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("hasElevateOrder", false);
        if (animator.GetBool("isUnderGround"))
            animator.SetBool("isUnderGround", false);
        else
            animator.SetBool("isUnderGround", true);
    }

}
