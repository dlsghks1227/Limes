using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NearestSearchComponent : SearchComponent
{
    #region variables
    [SerializeField]
    protected int searchAccuracy = 10;
    public int SearchAccuracy
    {
        get => searchAccuracy;
    }
    private Ray obstacleCheckRay;
    private Vector3 unitToEnemyVector;

    private DistancePriorityQueue nearestUnitQueue = new DistancePriorityQueue(Vector3.zero);
    private DistancePriorityQueue nearestBuildingQueue = new DistancePriorityQueue(Vector3.zero);
    #endregion
    #region user function
    protected override IEnumerator SearchUnits()
    {
        RaycastHit[] searchedObjects;
        RaycastHit searchedEnemyObject;
        while (true)
        {
            unitCheckLayer = GetTargetSearchLayer();
            nearestUnitQueue.ChangeStanrdardPosAndInit(transform.position);
            for (int i = searchAccuracy; i > 0; i--)
            {
                searchedObjects = Physics.SphereCastAll(transform.position, modifiedSearchRadius / i, Vector3.one, 0, unitCheckLayer);
                if (!(searchedObjects.Length ==0))
                    foreach (var a in searchedObjects)
                    {
                        SetObstacleCheckRay(a);
                        if (Physics.Raycast(obstacleCheckRay, unitToEnemyVector.magnitude + 1, Layers.OBSTACLE_CHECK_LAYER))
                            continue;
                        else
                        {
                            if (Physics.Raycast(obstacleCheckRay, out searchedEnemyObject, unitToEnemyVector.magnitude + 1, unitCheckLayer))
                            {
                                if (searchedEnemyObject.collider.CompareTag("Barricade"))
                                {
                                    EnqueueSearchObject(a.collider.transform);
                                    if (!a.collider.CompareTag("Barricade"))
                                        hasRecognizeEnemy = true;
                                }
                                else
                                {
                                    EnqueueSearchObject(searchedEnemyObject.collider.transform);
                                    hasRecognizeEnemy = true;
                                }
                            }
                        }
                    }
                else
                    hasRecognizeEnemy = false;
              
                if (hasRecognizeEnemy)
                    break;
            }
            yield return WaitTime.GetWaitForSecondOf(0.19f);
        }
    }

    private void SetObstacleCheckRay(RaycastHit info)
    {
        unitToEnemyVector = info.transform.position - transform.position;
        obstacleCheckRay.origin = transform.position;
        obstacleCheckRay.direction = unitToEnemyVector.normalized;
    }

    private void EnqueueSearchObject(Transform obj)
    {
        bool isStructure = obj.CompareTag("Building") || obj.CompareTag("Barricade");
        if (isStructure && !nearestBuildingQueue.IsContain(obj.transform))
            nearestBuildingQueue.Enqueue(obj);
        else if (obj.CompareTag("Unit") && !nearestUnitQueue.IsContain(obj.transform))
            nearestUnitQueue.Enqueue(obj);
    }

    public override GameObject GetSearchedUnit()
    {
        if (nearestUnitQueue.Count != 0)
        {
            var returnTransform = nearestUnitQueue.Dequeue();
            if (returnTransform)
                return returnTransform.gameObject;
        }
        return null;
    }

    public override GameObject GetSearchedBuilding()
    {
        if (nearestBuildingQueue.Count != 0)
        {
            var frontElement = nearestBuildingQueue.Dequeue();
            if(frontElement)
               return frontElement.gameObject;
        }
        return null;
    }
    #endregion

    private void OnEnable()
    {
        InitSearchRadius();
        StartCoroutine(SearchUnits()); 
    }
}
