using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirDropPocket : MonoBehaviour
{
    public Queue<GameObject> GeneratedUnits
    {
        get; set;
    }

    private Vector3 emergePos;
    protected Vector3 emergePosCollideCheckSize = new Vector3(0.1f, 0.1f, 0.1f);
    protected Vector3 emergePosBoxCastVec = Vector3.one.normalized;

    private int collideCheckCnt;
    private float emergePosAdjustDegree = 20f;
    private float generateDistance = 1f;

    private void OnCollisionEnter(Collision collision)
    {
        
            DeployUnits();
    }

    private void DeployUnits()
    {
        while (GeneratedUnits.Count != 0)
        {
            HandleEmergePosCollide();

            GameObject unit = GeneratedUnits.Dequeue();
            if (!unit)
                continue;
            unit.transform.position = emergePos;
            unit.SetActive(true);
        }
        Destroy(gameObject);
    }
    private void HandleEmergePosCollide()
    {
        RaycastHit[] dummyResult = new RaycastHit[1];
        emergePos = transform.position + transform.forward * generateDistance;
        collideCheckCnt = 0;

        while (Physics.BoxCastNonAlloc(emergePos, emergePosCollideCheckSize, emergePosBoxCastVec, dummyResult) != 0 || 
            Physics.RaycastNonAlloc(emergePos, Vector3.down, dummyResult, 5f) == 0)
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
        emergePos = transform.position + (Quaternion.AngleAxis(degree, Vector3.up) * transform.forward) * lengthFromBarrack;
    }
}
