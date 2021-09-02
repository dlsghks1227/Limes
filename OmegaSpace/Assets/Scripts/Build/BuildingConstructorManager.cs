using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using System;

class BuildingConstructorManager : MonoSingleton<BuildingConstructorManager>
{
    private List<BuildingConstructor> currentBuildingConstructor = new List<BuildingConstructor>(50);

    public void Build(BuildingData buildingData, Vector2Int pos, Grid _grid)
    {
        var buildingConstructor = new BuildingConstructor(buildingData, pos, _grid);
        bool isEnoupghIron = PlayerDataManager.instance.IsAffordableToPay(buildingData.IronCost);
        bool isEnoughGold = PlayerDataManager.instance.IsAffordableToPay(buildingData.GoldCost);
        if (buildingConstructor.IsContructable && isEnoupghIron && isEnoupghIron)
        {
            PlayerDataManager.instance.SubstractPlayerResource(buildingData.IronCost);
            PlayerDataManager.instance.SubstractPlayerResource(buildingData.GoldCost);

            StartCoroutine(buildingConstructor.BuildingAnimationRoutine);
            StartCoroutine(buildingConstructor.HealingWhileConstruct);
            currentBuildingConstructor.Add(buildingConstructor);
        }
    }

    public BuildingConstructor GetBuildingConstructorOf(GameObject obj)
    {
        foreach (var constructor in currentBuildingConstructor)
            if (constructor.Building == obj)
                return constructor;
        return null;
    }

    public void CancelConstruct(BuildingConstructor constructor)
    {
        if (currentBuildingConstructor.Contains(constructor) && constructor.IsOnConstructing)
        {
            StopCoroutine(constructor.BuildingAnimationRoutine);
            StopCoroutine(constructor.HealingWhileConstruct);
            Destroy(constructor.Building);
        }
    }

    public void CanCelConstruct(GameObject obj)
    {
        BuildingConstructor constructor;
        if ((constructor = GetBuildingConstructorOf(obj)) != null && constructor.IsOnConstructing)
        {
            StopCoroutine(constructor.BuildingAnimationRoutine);
            StopCoroutine(constructor.HealingWhileConstruct);
            Destroy(constructor.Building);
        }
    }
}

[Serializable]
public class BuildingConstructor
{
    private BuildingData currentBuildingData;
    private Vector2Int buildingPos;
    public GameObject Building
    {
        private set;
        get;
    }

    private Grid grid = null;

    private IEnumerator buildingAnimationRoutine;
    public IEnumerator BuildingAnimationRoutine
    {
        get
        {
            if (IsContructable)
            {
                if (buildingAnimationRoutine == null)
                    buildingAnimationRoutine = StartConstructAnimation();
                return buildingAnimationRoutine;
            }
            return null;
        }
    }
    private IEnumerator healingWhileConstruct;
    public IEnumerator HealingWhileConstruct
    {
        get
        {
            if (IsContructable)
            {
                if (healingWhileConstruct == null)
                    healingWhileConstruct = StartHealBuildingStat();
                return healingWhileConstruct;
            }
            return null;
        }
    }
    public bool IsContructable
    {
        get
        {
            //need grid system
            //return IsValidPos && !IsOnGenerating
            return true;
        }
    }
    public bool IsOnConstructing
    {
        private set;
        get;
    }

    public BuildingConstructor(BuildingData buildingData, Vector2Int pos, Grid _grid)
    {
        currentBuildingData = buildingData;
        buildingPos = pos;
        grid = _grid;
    }

    private IEnumerator StartConstructAnimation()
    {
        IEnumerator meshesEnumerator = currentBuildingData.MeshesWhileConstructing.GetEnumerator();
        int meshChangePeriod = (int)(currentBuildingData.BuildTime / currentBuildingData.MeshesWhileConstructing.Count);

        // 건물 생성 및 건설
        Building = GameObject.Instantiate(currentBuildingData.BuildingPrefab);
        grid.Locate(Building.GetComponent<BuildingEntity>(), buildingPos.x, buildingPos.y, Building.GetComponent<BuildingEntity>().GetBuildingType(), 10, true);

        Building.GetComponent<BoxCollider>().enabled = true;
        Building.layer = LayerMask.NameToLayer("Player");

        TechManager.Instance.AddPresentBuilding(Building);

        MeshFilter buildingMesh = Building.GetComponent<MeshFilter>();
        Mesh finalMesh = buildingMesh.mesh;

        IsOnConstructing = true;
        int second = 0;

        Building.GetComponent<BuildingEntity>().PlayCreateBuilding(currentBuildingData.BuildTime);

        while (second <= currentBuildingData.BuildTime)
        {
            if (second % meshChangePeriod == 0 && meshesEnumerator.MoveNext())
                buildingMesh.mesh = meshesEnumerator.Current as Mesh;

            yield return WaitTime.GetWaitForSecondOf(1f);
            second++;
        }
        buildingMesh.mesh = finalMesh;
        IsOnConstructing = false;
    }

    private IEnumerator StartHealBuildingStat()
    {
        BuildingEntity buildingEntity = Building.GetComponent<BuildingEntity>();
        buildingEntity.InitStatsAsZero();
        int hpMax = buildingEntity.HealthPointMax;
        int armorMax = buildingEntity.ArmorPointMax;

        int healPerDeciSec = (int)Math.Ceiling((hpMax + armorMax) / (currentBuildingData.BuildTime * 10));

        bool isNotFullHealth = buildingEntity.HealthPoint < buildingEntity.HealthPointMax;
        bool isNotFullArmor = buildingEntity.ArmorPoint < buildingEntity.ArmorPointMax;

        float second = 0;
        while ((isNotFullHealth || isNotFullArmor) && second <= currentBuildingData.BuildTime)
        {
            buildingEntity.RepairBuilding(healPerDeciSec);
            yield return WaitTime.GetWaitForSecondOf(0.1f);
            second += 0.1f;
        }
    }
}

