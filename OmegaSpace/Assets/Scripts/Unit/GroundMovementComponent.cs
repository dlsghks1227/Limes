using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class GroundMovementComponent : MovementComponent
{
    #region variables
    #endregion
    #region user functions
    public override bool Move(Vector3 pos, bool isPlayerOrder = false)
    {
        if (!agent)
            return false;

        DeleteMomentum();
        if (agent.SetDestination(pos))
        {
            transform.LookAt(pos);
            agent.isStopped = false;
            unitBackObstacle.enabled = false;
            moveState = EMoveState.MOVE;
            if (isPlayerOrder)
                moveState = EMoveState.playerOrder;
            return true;
        }
        else
        {
            Stop();
            return false;
        }
    }
    #endregion
}
