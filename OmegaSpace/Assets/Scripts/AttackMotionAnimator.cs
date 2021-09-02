using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMotionAnimator : StateMachineBehaviour
{
    private GroundMobileCombatAI combatAI;

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!combatAI)
            combatAI = animator.gameObject.GetComponent<GroundMobileCombatAI>();
  
        if (combatAI.PresentMoveState.Equals(EMoveState.MOVE))
        {
            animator.SetBool("isAttacking", false);
            return;
        }

        if (combatAI.IsAttacking)
            animator.SetBool("isAttacking", true);
        else
            animator.SetBool("isAttacking", false);
    }
}
