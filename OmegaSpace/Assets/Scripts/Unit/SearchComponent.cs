using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using UnityEditor;
using UnityEngine;
public static class Layers
{
    // int = 1 << (사용하고자 하는 레이어 넘버)
    public const int OBSTACLE_CHECK_LAYER = 1<<8;
    public const int PLAYER_UNIT_LAYER = 1<<9;
    public const int ENEMY_UNIT_LAYER = 1<<10;
}
public class SearchComponent :MonoBehaviour
{
    #region variables
    [SerializeField]
    protected int searchLayerNum;
    [SerializeField]
    protected float searchRadius = 50f;
    public float SearchRadius
    {
        get => searchRadius;
    }
    protected float searchRadiusCoefficient = 1f;
    protected float modifiedSearchRadius;
    public float ModifiedSearchRadius
    {
        get => modifiedSearchRadius;
    }
    protected bool hasRecognizeEnemy = false;
    public bool HasRecognizeEnemy
    {
        get => hasRecognizeEnemy;
    }
    protected Queue<GameObject> searchedEnemies = new Queue<GameObject>();
    protected Queue<GameObject> searchedBuildings = new Queue<GameObject>();
    protected int unitCheckLayer;       
    public int UnitCheckLayer
    {
        get => unitCheckLayer;
    }
    #endregion
    #region user function
    public void InitSearchRadius()
    {
        modifiedSearchRadius = searchRadius;
    }

    public void AdjustSearchRadius(float ratio)
    {
        searchRadiusCoefficient += ratio;
        modifiedSearchRadius = searchRadius * searchRadiusCoefficient;
    }

    public void AdjustSearchRadius(int value)
    {
        searchRadius += value;
        modifiedSearchRadius = searchRadius * searchRadiusCoefficient;
    }

    public virtual GameObject GetSearchedUnit()
    {
        if (searchedEnemies.Count != 0)
            return searchedEnemies.Dequeue();
        return null;
    }

    public virtual GameObject GetSearchedBuilding()
    {
        if (searchedBuildings.Count != 0)
            return searchedBuildings.Dequeue();
        return null;
    }

    protected virtual IEnumerator SearchUnits() { yield return WaitTime.GetWaitForSecondOf(0.1f); }

    protected int GetTargetSearchLayer()
    {
        if (gameObject.layer == 9)
            return Layers.ENEMY_UNIT_LAYER;
        else if (gameObject.layer == 10)
            return Layers.PLAYER_UNIT_LAYER;
        else
            return 0;
    }
    #endregion  
}

