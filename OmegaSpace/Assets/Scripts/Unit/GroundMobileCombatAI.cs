using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class GroundMobileCombatAI : CombatAI
{
    #region variables
    private MovementComponent movementComponent;
    private Animator unitAnimaotr;  
    [SerializeField]
    public EMoveState PresentMoveState
    {
        get => movementComponent.MoveState;
    }
    private float retreatDistance = 15f;
    public float RetreatDistance
    {
        get => retreatDistance;
    }
    private bool IsMovableToAttack
    {
        get => !movementComponent.MoveState.Equals(EMoveState.HOLD);
    }
    public bool IsIdle
    {
        private set;
        get;
    }
    #endregion

    #region user function
    protected override IEnumerator ActiveCombatMode()
    {
        Vector3 targetLocation;
        string buildingTag=null;
        while (true)
        {
            if (attackComponent.attackTarget)
            {
                IsIdle = false;
                targetLocation = attackComponent.attackTarget.transform.position;
                if (IsObstacleOnTheWay(targetLocation))
                {
                    if (IsMovableToAttack && !movementComponent.Move(targetLocation))
                        Retreat(targetLocation);
                }
                else if (IsEnemyBarricadeOnTheWay(targetLocation))
                {
                    if (attackComponent.IsInRange(targetLocation))
                    {
                        //yield return WaitTime.GetWaitForSecondOf(0.8f);
                        StartActualAttack();
                        yield return WaitTime.GetWaitForSecondOf(attackComponent.ModifiedAttackSpeed);
                        attackComponent.Attack(attackComponent.attackTarget);
                        IsAttacking = false;
                        continue;
                    }
                    else if (IsMovableToAttack)
                        movementComponent.Move(GetNearAttackPostion(targetLocation));
                }
                else if (IsMyBuildingOnTheWay(targetLocation,ref buildingTag))
                {
                    if (buildingTag.Equals("Barricade"))
                    {
                        if(attackComponent.IsInRange(targetLocation))
                        {
                            StartActualAttack();
                            yield return WaitTime.GetWaitForSecondOf(attackComponent.ModifiedAttackSpeed);
                            attackComponent.Attack(attackComponent.attackTarget);
                            IsAttacking = false;
                            continue;
                        }
                       else if (IsMovableToAttack)
                            movementComponent.Move(GetNearAttackPostion(targetLocation));
                    }
                    else
                    {
                        if (IsMovableToAttack && !movementComponent.Move(targetLocation))
                            continue;
                    }
                }
                else
                {
                    if (attackComponent.IsInRange(targetLocation))
                    {
                        StartActualAttack();
                        yield return WaitTime.GetWaitForSecondOf(attackComponent.ModifiedAttackSpeed);
                        attackComponent.Attack(attackComponent.attackTarget);
                        IsAttacking = false;
                        continue;
                    }
                    else if (IsMovableToAttack)
                        movementComponent.Move(targetLocation);
                }
            }
            else if(movementComponent.MoveState.Equals(EMoveState.playerOrder))
                IsIdle = true;
            else
            {
                IsIdle = true;
                movementComponent.Stop();
            }
            yield return WaitTime.GetWaitForSecondOf(0.2f);
        }
    }
    
    private void StartActualAttack()
    {
        movementComponent.Stop();
        //attackComponent.Attack(attackComponent.attackTarget);
        IsAttacking = true;
    }

    private bool IsObstacleOnTheWay(Vector3 targetPos)
    {
        Vector3 unitToTargetVec = targetPos - transform.position;
        RaycastHit[] dummyResult = new RaycastHit[1];
       
        return Physics.RaycastNonAlloc(transform.position, unitToTargetVec.normalized, dummyResult, unitToTargetVec.magnitude, Layers.OBSTACLE_CHECK_LAYER) != 0;
    }

    private bool IsEnemyBarricadeOnTheWay(Vector3 targetPos)
    {
        RaycastHit obstacleInfo;
        if (Physics.Linecast(transform.position, targetPos, out obstacleInfo, searchComponent.UnitCheckLayer) && obstacleInfo.collider.CompareTag("Barricade"))
            return true;
        else
            return false;
    }

    private bool IsMyBuildingOnTheWay(Vector3 targetPos,ref string buildingTag)
    {
        RaycastHit[] hitResultArr = new RaycastHit[30];
        Vector3 unitToTargetVec = targetPos - transform.position;
        int myLayer = searchComponent.UnitCheckLayer == Layers.ENEMY_UNIT_LAYER ? Layers.PLAYER_UNIT_LAYER : Layers.ENEMY_UNIT_LAYER;

        bool isHitSomething = Physics.RaycastNonAlloc(transform.position, unitToTargetVec.normalized, hitResultArr, unitToTargetVec.magnitude, myLayer) != 0;
        buildingTag = null;

        if (isHitSomething)
        {
            for(int i=0; hitResultArr[i].collider != null; i++)
                if (hitResultArr[i].collider.CompareTag("Building"))
                {
                    buildingTag = "Building";
                    break;
                }
                else if(hitResultArr[i].collider.CompareTag("Barricade"))
                    buildingTag = "Barricade";

            if (buildingTag != null)
                return true;
        }
        return false;
    }

    private Vector3 GetNearAttackPostion(Vector3 attackTargetPos)
    {
        Vector3 targetToMeDirection = (transform.position - attackTargetPos).normalized;   
        return (attackTargetPos + targetToMeDirection * attackComponent.AttackRange);
    } 
    
    private void Retreat(Vector3 standardPos)
    {
        Vector3 retreatDirection = (transform.position - standardPos).normalized;
        Vector3 retreatPos;
        RaycastHit obstacleInfo;
        attackComponent.InitAttackTarget();

        if (Physics.Raycast(transform.position, retreatDirection, out obstacleInfo, retreatDistance, Layers.OBSTACLE_CHECK_LAYER))
            retreatPos = transform.position + retreatDirection * (obstacleInfo.distance - 2);
        else
            retreatPos = transform.position + retreatDirection * retreatDistance;

        movementComponent.Move(retreatPos);
    }
    #endregion
    private void OnEnable()
    {
        attackComponent = GetComponent<AttackComponent>();
        searchComponent = GetComponent<SearchComponent>();
        movementComponent = GetComponent<MovementComponent>();
        StartCoroutine(AutoSetAttackTarget());
        StartCoroutine(ActiveCombatMode());
    }
}
