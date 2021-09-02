using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using UnityEngine.Serialization;


public enum EMoveState
{
    STOP,
    MOVE,
    HOLD,
    playerOrder
}


[RequireComponent(typeof(NavMeshAgent))]
public class MovementComponent : MonoBehaviour
{
    #region variables
    protected EMoveState moveState = EMoveState.STOP;
    [SerializeField]
    protected float speed;
    protected float speedCoefficient = 1f;
    protected float modifiedSpeed;

    protected Rigidbody unitRigidBody = null;

    public EMoveState MoveState
    {
        get => moveState;
    }
    public float Speed
    {
        get => speed;
    }
    public float ModifiedSpeed
    {
        get => modifiedSpeed;
    }

    [SerializeField]
    protected NavMeshAgent agent;
    protected NavMeshObstacle unitBackObstacle;

    #endregion
    #region user funtions
    public void AdjustSpeed(float ratio)
    {
        speedCoefficient += ratio;
        modifiedSpeed = speed * speedCoefficient;
        agent.speed = modifiedSpeed;
    }

    public void AdjustSpeed(int value)
    {
        speed += value;
        modifiedSpeed = speed * speedCoefficient;
        agent.speed = modifiedSpeed;

    }

    public virtual bool Move(Vector3 pos, bool isPlayerOrder = false)
    {
        if (!agent)
            return false;
       

        DeleteMomentum();
        if (agent.SetDestination(pos))
        {
            transform.LookAt(pos);
            agent.isStopped = false;
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

    public void Stop()
    {
        if (!agent)
            return;

        DeleteMomentum();
        agent.isStopped = true;
        unitBackObstacle.enabled = true;
        moveState = EMoveState.STOP;
    }

    protected void DeleteMomentum()
    {
        unitRigidBody.velocity = Vector3.zero;
        unitRigidBody.angularVelocity = Vector3.zero;
    }

    public void Hold()
    {
        if (!agent)
            return;

        Stop();
        moveState = EMoveState.HOLD;
    }

    protected void InitSpeed()
    {
        modifiedSpeed = speed * speedCoefficient;
        agent.speed = modifiedSpeed;
    }
    #endregion
    protected void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        unitRigidBody = GetComponent<Rigidbody>();
        unitBackObstacle = transform.GetChild(0).GetComponent<NavMeshObstacle>();
        InitSpeed();
        Stop();
    }
}
