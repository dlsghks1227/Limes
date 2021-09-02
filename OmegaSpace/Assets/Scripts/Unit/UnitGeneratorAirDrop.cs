using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.InputSystem;


public class UnitGeneratorAirDrop  : UnitGeneratorBase
{
    private MiniMapManager miniMapManager;
    #region variables
    [SerializeField]
    private float fallHeight = 20f;
    public float FallHeight
    {
        get => fallHeight;
    }

    [SerializeField]
    private GameObject airPocketPrefab;
    public int AirPoketSize
    {
        get;set;
    }

    #endregion
    #region user functions
    protected override IEnumerator DeployUnit()
    {
        miniMapManager = new MiniMapManager();
        
        while (generatingQueue.Count != 0)
        {
            IsOnGenerating = true;
            
            UnitData generatingUnit = generatingQueue.Dequeue();
            GameObject unit = Instantiate(generatingUnit.Prefab, Vector3.zero, Quaternion.identity);
            unit.SetActive(false);
            unit.layer = gameObject.layer;
            miniMapManager.SetColor(unit);

            AddUnitsToGeneratedUnits(unit);
            yield return WaitTime.GetWaitForSecondOf(1f);
        }
        LaunchAirDropPocketUnitContainer();
       
        IsOnGenerating = false;
    }

    private void LaunchAirDropPocketUnitContainer()
    {
        Vector3 emergePosInAir = emergePos;
        emergePosInAir.y += fallHeight;

        Queue<GameObject> airPocketUnitContainer = new Queue<GameObject>(30);
        for (int i = 0; i < generatableUnits.Count && generatableUnits[i]; i++)
        {
            var list = GetGeneratedUnitListOf(generatableUnits[i]);
            if (list != null && list.Count != 0)
                foreach (var a in list)
                    airPocketUnitContainer.Enqueue(a);
        }

        if (airPocketUnitContainer.Count != 0) 
        {
            var airPocket = Instantiate(airPocketPrefab, emergePosInAir, Quaternion.identity);
            airPocket.GetComponent<AirDropPocket>().GeneratedUnits = airPocketUnitContainer;
        }
    }
 
    #endregion
    private void OnEnable()
    {
        GetUnitDatas();
        InitGeneratingQueueSize();
    }
}

