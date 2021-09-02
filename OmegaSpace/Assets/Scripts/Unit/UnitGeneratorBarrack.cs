using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitGeneratorBarrack : UnitGeneratorBase
{
    private MiniMapManager miniMapManager;

    #region variables
    [SerializeField]
    private float emergePosAdjustDegree = 40;
    public float generateDistance = 5f;
    private int collideCheckCnt;
    #endregion

    protected override IEnumerator DeployUnit()
    {
        miniMapManager = new MiniMapManager();
        while (generatingQueue.Count != 0)
        {
            IsOnGenerating = true;
            currentGeneratingUnit = generatingQueue.Dequeue();
            yield return WaitTime.GetWaitForSecondOf(currentGeneratingUnit.generateTimes);

            HandleEmergePosCollide();
            GameObject unit = Instantiate(currentGeneratingUnit.Prefab, emergePos, Quaternion.identity);
            unit.SetActive(false);
            unit.layer = gameObject.layer;
            unit.gameObject.GetComponent<UnitEntity>().SetMaterial(gameObject.layer == LayerMask.NameToLayer("Player") ? true : false);
            miniMapManager.SetColor(unit);
            AddUnitsToGeneratedUnits(unit);
            unit.SetActive(true);
        }
        IsOnGenerating = false;
    }

    private void HandleEmergePosCollide()
    {
        RaycastHit[] dummyResult = new RaycastHit[1];
        emergePos = (transform.position + new Vector3(0.0f, 0.5f, 0.0f)) + transform.forward * generateDistance;
        collideCheckCnt = 0;

        while (Physics.BoxCastNonAlloc(emergePos, emergePosCollideCheckSize, emergePosBoxCastVec, dummyResult) != 0 ||
            Physics.RaycastNonAlloc(emergePos, Vector3.down, dummyResult, 10f) == 0)
        {
            collideCheckCnt++;
            AdjustEmergePos(emergePosAdjustDegree * collideCheckCnt);
            if (collideCheckCnt > 15)
                break;
        }
    }

    private void AdjustEmergePos(float degree)
    {
        float lengthFromBarrack = generateDistance;
        int checkRotateCnt = (collideCheckCnt * (int)emergePosAdjustDegree) / 360;
        if (checkRotateCnt > 0)
            lengthFromBarrack *= 1.5f * checkRotateCnt;
        emergePos = (transform.position + new Vector3(0.0f, 0.5f, 0.0f)) + (Quaternion.AngleAxis(degree, Vector3.up) * transform.forward) * lengthFromBarrack;
    }

    private void OnStart()
    {
        GetUnitDatas();
    }
}

