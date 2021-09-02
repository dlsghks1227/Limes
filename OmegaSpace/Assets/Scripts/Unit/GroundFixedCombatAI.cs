using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundFixedCombatAI : CombatAI
{
    RaycastHit[] hitResultsForCheckMyBuilding = new RaycastHit[30];

    protected override IEnumerator ActiveCombatMode()
    {
        while (true)
        {
            if (attackComponent.attackTarget)
            {
                Vector3 targetLocation = attackComponent.attackTarget.transform.position;
                if (attackComponent.IsInRange(targetLocation))
                {
                    if (IsObstacleOnTheWay(targetLocation))
                    {
                        yield return WaitTime.GetWaitForSecondOf(0.1f);
                        continue;
                    }
                    else if (IsMyBuildingOnTheWay(targetLocation))
                    {
                        yield return WaitTime.GetWaitForSecondOf(0.1f);
                        continue;
                    }
                    else
                    {
                        attackComponent.Attack(attackComponent.attackTarget);
                        yield return WaitTime.GetWaitForSecondOf(attackComponent.ModifiedAttackSpeed);
                        continue;
                    }
                }
            }
            yield return WaitTime.GetWaitForSecondOf(0.1f);
        }
    }

    private bool IsObstacleOnTheWay(Vector3 targetLocation)
    {
        Vector3 unitToTargetVec = targetLocation - transform.position;
        RaycastHit[] dummyResult = new RaycastHit[1];
       
        return (Physics.RaycastNonAlloc(transform.position, unitToTargetVec.normalized, dummyResult, unitToTargetVec.magnitude, Layers.OBSTACLE_CHECK_LAYER) != 0);
    }

    private bool IsMyBuildingOnTheWay(Vector3 targetLocation)
    {
        hitResultsForCheckMyBuilding.Initialize();
        Vector3 unitToTargetVec = targetLocation - transform.position;
        int myLayer = 1 << gameObject.layer;
        bool isHitSomething = Physics.RaycastNonAlloc(transform.position, unitToTargetVec.normalized, hitResultsForCheckMyBuilding, unitToTargetVec.magnitude, myLayer) != 0;

        if (isHitSomething)
            for (int i = 0; i < hitResultsForCheckMyBuilding.Length && hitResultsForCheckMyBuilding[i].collider != null; i++)
                if (hitResultsForCheckMyBuilding[i].collider.CompareTag("Building"))
                    return true;
        return false;
    }

    private void OnEnable()
    {
        attackComponent = GetComponent<AttackComponent>();
        searchComponent = GetComponent<SearchComponent>();
        StartCoroutine(AutoSetAttackTarget());
        StartCoroutine(ActiveCombatMode());
    }
}
