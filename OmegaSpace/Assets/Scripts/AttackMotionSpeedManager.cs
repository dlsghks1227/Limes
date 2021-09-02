using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMotionSpeedManager : StateMachineBehaviour
{
    private AttackComponent attackComponent;
    private float animationSpeedRatioToAttackSpeed;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!attackComponent)
            attackComponent = animator.gameObject.GetComponent<AttackComponent>();
        float attackSpeed = attackComponent.ModifiedAttackSpeed;

        animationSpeedRatioToAttackSpeed = stateInfo.length / (1 / attackSpeed);
        animator.speed /= animationSpeedRatioToAttackSpeed;
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.speed *= animationSpeedRatioToAttackSpeed;
    }
}
